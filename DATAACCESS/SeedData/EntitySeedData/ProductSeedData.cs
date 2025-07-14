using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Concrete;
using CORE.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DATAACCESS.SeedData.EntitySeedData
{
    public class ProductSeedData : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                    new Product
                    {
                        Id = Guid.Parse("p0000000-0000-0000-0000-000000000001"),
                        ProductName = "HP Laptop 15s",
                        ImagePath = null,
                        StockAmount = 25,
                        SubCategoryId = Guid.Parse("c0000000-0000-0000-0000-000000000001"),
                        CreatedDate = DateTime.Parse("2025-07-01"),
                        UpdatedDate = null,
                        DeletedDate = null,
                        Status = Status.Active
                    },
    new Product
    {
        Id = Guid.Parse("p0000000-0000-0000-0000-000000000002"),
        ProductName = "Logitech M720 Mouse",
        ImagePath = null,
        StockAmount = 100,
        SubCategoryId = Guid.Parse("c0000000-0000-0000-0000-000000000002"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Product
    {
        Id = Guid.Parse("p0000000-0000-0000-0000-000000000003"),
        ProductName = "Dell Monitor 24''",
        ImagePath = null,
        StockAmount = 40,
        SubCategoryId = Guid.Parse("c0000000-0000-0000-0000-000000000003"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Product
    {
        Id = Guid.Parse("p0000000-0000-0000-0000-000000000004"),
        ProductName = "Epson L3250 Yazıcı",
        ImagePath = null,
        StockAmount = 10,
        SubCategoryId = Guid.Parse("c0000000-0000-0000-0000-000000000004"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Product
    {
        Id = Guid.Parse("p0000000-0000-0000-0000-000000000005"),
        ProductName = "TP-Link Router AX1800",
        ImagePath = null,
        StockAmount = 60,
        SubCategoryId = Guid.Parse("c0000000-0000-0000-0000-000000000005"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Product
    {
        Id = Guid.Parse("p0000000-0000-0000-0000-000000000006"),
        ProductName = "Kingston 16GB USB 3.0",
        ImagePath = null,
        StockAmount = 200,
        SubCategoryId = Guid.Parse("c0000000-0000-0000-0000-000000000002"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Product
    {
        Id = Guid.Parse("p0000000-0000-0000-0000-000000000007"),
        ProductName = "Samsung SSD 1TB",
        ImagePath = null,
        StockAmount = 30,
        SubCategoryId = Guid.Parse("c0000000-0000-0000-0000-000000000006"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Product
    {
        Id = Guid.Parse("p0000000-0000-0000-0000-000000000008"),
        ProductName = "Lenovo ThinkPad T14",
        ImagePath = null,
        StockAmount = 12,
        SubCategoryId = Guid.Parse("c0000000-0000-0000-0000-000000000001"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Product
    {
        Id = Guid.Parse("p0000000-0000-0000-0000-000000000009"),
        ProductName = "Xiaomi Mi Kamera 360",
        ImagePath = null,
        StockAmount = 35,
        SubCategoryId = Guid.Parse("c0000000-0000-0000-0000-000000000007"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Product
    {
        Id = Guid.Parse("p0000000-0000-0000-0000-000000000010"),
        ProductName = "Asus Gaming Mouse",
        ImagePath = null,
        StockAmount = 70,
        SubCategoryId = Guid.Parse("c0000000-0000-0000-0000-000000000002"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    }
            );
        }
    }
}
