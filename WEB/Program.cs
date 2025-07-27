using Autofac;
using Autofac.Extensions.DependencyInjection;
using DATAACCESS.Context;
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
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Anasayfa}/{action=Index}/{id?}");

app.Run();
