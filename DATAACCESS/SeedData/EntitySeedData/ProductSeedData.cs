using System;
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
                new Product { Id = Guid.Parse("11111111-aaaa-bbbb-cccc-111111111111"), ProductName = "Defibrilatör Cihazı (D-100)", ImagePath = null, StockAmount = 3, SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-000000000001"), CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
                new Product { Id = Guid.Parse("22222222-aaaa-bbbb-cccc-222222222222"), ProductName = "Tek Kullanımlık Enjektör 5 ml", ImagePath = null, StockAmount = 500, SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-000000000002"), CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
                new Product { Id = Guid.Parse("33333333-aaaa-bbbb-cccc-333333333333"), ProductName = "Otoklav Poşeti 200x300 mm", ImagePath = null, StockAmount = 200, SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-000000000003"), CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
                new Product { Id = Guid.Parse("44444444-aaaa-bbbb-cccc-444444444444"), ProductName = "Biyokimya Reaktifi Seti", ImagePath = null, StockAmount = 25, SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-000000000004"), CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
                new Product { Id = Guid.Parse("55555555-aaaa-bbbb-cccc-555555555555"), ProductName = "IV Serum Seti", ImagePath = null, StockAmount = 120, SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-000000000005"), CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
                new Product { Id = Guid.Parse("66666666-aaaa-bbbb-cccc-666666666666"), ProductName = "Kurşun Önlük (Yetişkin)", ImagePath = null, StockAmount = 10, SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-000000000006"), CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
                new Product { Id = Guid.Parse("77777777-aaaa-bbbb-cccc-777777777777"), ProductName = "Ortopedik Kol Ateli", ImagePath = null, StockAmount = 40, SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-000000000007"), CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
                new Product { Id = Guid.Parse("88888888-aaaa-bbbb-cccc-888888888888"), ProductName = "Anestezi Maskesi (Medium)", ImagePath = null, StockAmount = 60, SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-000000000008"), CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
                new Product { Id = Guid.Parse("99999999-aaaa-bbbb-cccc-999999999999"), ProductName = "Ventilatör Devresi (Yetişkin)", ImagePath = null, StockAmount = 30, SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-000000000009"), CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
                new Product { Id = Guid.Parse("aaaaaaaa-aaaa-bbbb-cccc-aaaaaaaaaaaa"), ProductName = "Diş Hekimliği El Aleti Seti", ImagePath = null, StockAmount = 15, SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-000000000010"), CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active }
            );
        }
    }
}
