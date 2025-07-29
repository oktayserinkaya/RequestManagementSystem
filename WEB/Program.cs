using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DATAACCESS.Context;
using FluentValidation;
using FluentValidation.AspNetCore;
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


// Eðer ki FluentValidation kullanmak isteniyorsa aþaðýdaki 3 methodu kullanmak zorundayýz!!!

// Abstract validator'dan miras almýþ validator'larý bulup otomatik olarak sisteme ekler. Örnek : CreateDepartmentValidator
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// FluentValidation'ý ASP .NET Core model binding sürecine entegre eder. Yani MVC Controller üzerinden gelen request'lerin otomatik olarak validasyonunu saðlar.
builder.Services.AddFluentValidationAutoValidation();

// FluentValidation'ýn Client-Side(Ön Yüz) validasyon desteðiniz saðlar. MVC View'lerde kullanýlýr.
builder.Services.AddFluentValidationClientsideAdapters();


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
  pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
