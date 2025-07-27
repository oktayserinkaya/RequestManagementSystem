using Autofac;
using Autofac.Extensions.DependencyInjection;
using DATAACCESS.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using WEB.Autofac;

var builder = WebApplication.CreateBuilder(args);


// Yaþam Döngüsünü Tanýmladýk (Life Cycle)
// AutofacServiceProviderFactory : Bu kodun çalýþmasý için Autofac.Extensions.DependencyInjection paketinin kurulmasý gerekir!! 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacModule());
});

// Add services to the container.
builder.Services.AddControllersWithViews();

// Baðlantý cümlesini oku
var entitySQLConnection = builder.Configuration.GetConnectionString("EntityPostgreSQLConnection");

// DbContext'i DI sistemine ekle
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(entitySQLConnection);
});

// Uygulamayý oluþtur (Build iþlemi buraya kadar yapýlandýrýlmýþ tüm servisleri içerir)
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Anasayfa}/{action=Index}/{id?}");

app.Run();
