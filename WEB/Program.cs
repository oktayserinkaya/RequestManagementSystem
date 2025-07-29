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


// Ya�am D�ng�s�n� Tan�mlad�k (Life Cycle)
// AutofacServiceProviderFactory : Bu kodun �al��mas� i�in Autofac.Extensions.DependencyInjection paketinin kurulmas� gerekir!! 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new AutofacModule());
});


// E�er ki FluentValidation kullanmak isteniyorsa a�a��daki 3 methodu kullanmak zorunday�z!!!

// Abstract validator'dan miras alm�� validator'lar� bulup otomatik olarak sisteme ekler. �rnek : CreateDepartmentValidator
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// FluentValidation'� ASP .NET Core model binding s�recine entegre eder. Yani MVC Controller �zerinden gelen request'lerin otomatik olarak validasyonunu sa�lar.
builder.Services.AddFluentValidationAutoValidation();

// FluentValidation'�n Client-Side(�n Y�z) validasyon deste�iniz sa�lar. MVC View'lerde kullan�l�r.
builder.Services.AddFluentValidationClientsideAdapters();


// Add services to the container.
builder.Services.AddControllersWithViews();

// Ba�lant� c�mlesini oku
var entitySQLConnection = builder.Configuration.GetConnectionString("EntityPostgreSQLConnection");

// DbContext'i DI sistemine ekle
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(entitySQLConnection);
});

// Uygulamay� olu�tur (Build i�lemi buraya kadar yap�land�r�lm�� t�m servisleri i�erir)
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
