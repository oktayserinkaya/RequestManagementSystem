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
        public DbSet<Purchase> Purchases { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ---------- Request ----------
            modelBuilder.Entity<Request>(entity =>
            {
                // Identity tarafındaki AppUser'a referans gölge sütun
                entity.Property<Guid>("AppUserId");

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

            // ---------- Purchase (Request ile 1–1) ----------
            modelBuilder.Entity<Purchase>(b =>
            {
                // 1–1 ilişki: Purchase(RequestId) -> Request(Id)
                b.HasOne(p => p.Request)
                 .WithOne(r => r.Purchase)
                 .HasForeignKey<Purchase>(p => p.RequestId)
                 .OnDelete(DeleteBehavior.Cascade);

                // 1–1'i garantiye almak için unique index
                b.HasIndex(p => p.RequestId).IsUnique();

                // Para alanları için hassasiyet
                b.Property(p => p.UnitPrice).HasColumnType("numeric(18,2)");
                b.Property(p => p.Subtotal).HasColumnType("numeric(18,2)");
                b.Property(p => p.DiscountAmount).HasColumnType("numeric(18,2)");
                b.Property(p => p.VatAmount).HasColumnType("numeric(18,2)");
                b.Property(p => p.GrandTotal).HasColumnType("numeric(18,2)");

                // Yüzde alanları (opsiyon) — istersen decimal(5,2) yapabilirsin
                b.Property(p => p.DiscountRate).HasColumnType("numeric(5,2)");
                b.Property(p => p.VatRate).HasColumnType("numeric(5,2)");

                // Opsiyon: string uzunlukları
                b.Property(p => p.Currency).HasMaxLength(8);
                b.Property(p => p.SupplierName).HasMaxLength(200);
                b.Property(p => p.SupplierTaxNo).HasMaxLength(20);
                b.Property(p => p.SupplierIban).HasMaxLength(34);
                b.Property(p => p.SupplierEmail).HasMaxLength(200);
                b.Property(p => p.SupplierPhone).HasMaxLength(30);
                b.Property(p => p.OfferPdfPath).HasMaxLength(500);
            });

            // ---------- Seed Data ----------
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
