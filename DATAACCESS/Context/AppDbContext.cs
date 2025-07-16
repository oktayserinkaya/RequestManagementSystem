using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Concrete;
using DATAACCESS.SeedData.EntitySeedData;
using Microsoft.EntityFrameworkCore;

namespace DATAACCESS.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        static AppDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategorySeedData());
            modelBuilder.ApplyConfiguration(new TitleSeedData());
            modelBuilder.ApplyConfiguration(new DepartmentSeedData());

            modelBuilder.ApplyConfiguration(new SubCategorySeedData());

            modelBuilder.ApplyConfiguration(new EmployeeSeedData());

            modelBuilder.ApplyConfiguration(new ProductSeedData());
            modelBuilder.ApplyConfiguration(new RequestSeedData());
            modelBuilder.ApplyConfiguration(new PaymentSeedData());

            modelBuilder.ApplyConfiguration(new WarehouseSeedData());


        }
    }
}
