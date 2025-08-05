using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DATAACCESS.Context;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using WEB.Autofac;
using Microsoft.Extensions.Logging; // ✅ Gerekli namespace

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
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

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
