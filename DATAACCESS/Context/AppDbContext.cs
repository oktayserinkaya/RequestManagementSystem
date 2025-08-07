using CORE.Entities.Concrete;
using CORE.IdentityEntities;
using DATAACCESS.SeedData.EntitySeedData;
using Microsoft.EntityFrameworkCore;

namespace DATAACCESS.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
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

            // Request entity konfigürasyonu
            modelBuilder.Entity<Request>(entity =>
            {
                // AppUserId için shadow property (diğer veritabanındaki AppUser'a referans)
                entity.Property<Guid>("AppUserId");

                // Diğer ilişkiler
                entity.HasOne(r => r.Employee)
                    .WithMany()
                    .HasForeignKey(r => r.EmployeeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Product)
                    .WithMany()
                    .HasForeignKey(r => r.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Title)
                    .WithMany()
                    .HasForeignKey(r => r.TitleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Seed data konfigürasyonları
            modelBuilder.ApplyConfiguration(new EmployeeSeedData());
            modelBuilder.ApplyConfiguration(new ProductSeedData());
            modelBuilder.ApplyConfiguration(new TitleSeedData());
            modelBuilder.ApplyConfiguration(new DepartmentSeedData());
            modelBuilder.ApplyConfiguration(new RequestSeedData());
            modelBuilder.ApplyConfiguration(new CategorySeedData());
            modelBuilder.ApplyConfiguration(new SubCategorySeedData());
            modelBuilder.ApplyConfiguration(new PaymentSeedData());
            modelBuilder.ApplyConfiguration(new WarehouseSeedData());
        }
    }
}