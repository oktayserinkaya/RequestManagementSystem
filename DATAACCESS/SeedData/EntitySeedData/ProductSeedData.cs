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
                    Id = Guid.Parse("11111111-aaaa-bbbb-cccc-111111111111"),
                    ProductName = "Canon EOS 2000D Kamera",
                    ImagePath = null,
                    StockAmount = 8,
                    SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-0000000000ab"),
                    CreatedDate = DateTime.Parse("2025-07-01"),
                    Status = Status.Active
                },
                new Product
                {
                    Id = Guid.Parse("22222222-aaaa-bbbb-cccc-222222222222"),
                    ProductName = "Apple Magic Keyboard",
                    ImagePath = null,
                    StockAmount = 20,
                    SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-0000000000ac"),
                    CreatedDate = DateTime.Parse("2025-07-01"),
                    Status = Status.Active
                },
                new Product
                {
                    Id = Guid.Parse("33333333-aaaa-bbbb-cccc-333333333333"),
                    ProductName = "Acer Nitro 5 Gaming Laptop",
                    ImagePath = null,
                    StockAmount = 5,
                    SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-0000000000ad"),
                    CreatedDate = DateTime.Parse("2025-07-01"),
                    Status = Status.Active
                },
                new Product
                {
                    Id = Guid.Parse("44444444-aaaa-bbbb-cccc-444444444444"),
                    ProductName = "JBL Bluetooth Hoparlör",
                    ImagePath = null,
                    StockAmount = 50,
                    SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-0000000000ae"),
                    CreatedDate = DateTime.Parse("2025-07-01"),
                    Status = Status.Active
                }
            );
        }
    }




}
