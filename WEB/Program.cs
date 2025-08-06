using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CORE.IdentityEntities;
using DATAACCESS.Context;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // ✅ Gerekli namespace
using WEB.Autofac;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Logging (DEBUG için)
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// 🔹 Autofac Container
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
       .ConfigureContainer<ContainerBuilder>(containerBuilder =>
       {
           containerBuilder.RegisterModule(new AutofacModule());
       });

// 🔹 FluentValidation
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

// 🔹 Session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 🔹 HttpContextAccessor (Layout.cshtml için gerekli!)
builder.Services.AddHttpContextAccessor();

// 🔹 MVC
builder.Services.AddControllersWithViews();

// 🔹 DbContext
var connectionString = builder.Configuration.GetConnectionString("EntityPostgreSQLConnection");
var identityConnectionString = builder.Configuration.GetConnectionString("IdentityPostgreSQLConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    options.UseNpgsql(identityConnectionString);
});

builder.Services.AddIdentity<AppUser, AppRole>(x =>
{
    x.SignIn.RequireConfirmedPhoneNumber = false;
    x.SignIn.RequireConfirmedEmail = false;
    x.SignIn.RequireConfirmedAccount = false;
    x.User.RequireUniqueEmail = true;
    x.Password.RequiredLength = 3;
    x.Password.RequiredUniqueChars = 0;
    x.Password.RequireNonAlphanumeric = false;
    x.Password.RequireUppercase = false;
    x.Password.RequireLowercase = false;
    x.Lockout.MaxFailedAccessAttempts = 5; // Şifreyi 5 kere yanlış girerse
    x.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // 5 dakika boyunca hesabı dondur. 
}).AddEntityFrameworkStores<AppIdentityDbContext>()
.AddDefaultTokenProviders();


var app = builder.Build();

// 🔹 Middleware Pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 🔹 Custom Logging Middleware (opsiyonel)
app.Use(async (context, next) =>
{
    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("➡️ HTTP Request: {Method} {Path}", context.Request.Method, context.Request.Path);
    await next();
    logger.LogInformation("⬅️ HTTP Response: {StatusCode}", context.Response.StatusCode);
});

app.UseSession();
app.UseAuthorization();

// 🔹 Route Ayarları
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
