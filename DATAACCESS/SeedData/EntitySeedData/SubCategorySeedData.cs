using System;
using CORE.Entities.Concrete;
using CORE.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DATAACCESS.SeedData.EntitySeedData
{
    public class SubCategorySeedData : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasData(
                new SubCategory { Id = Guid.Parse("0f111111-0000-0000-0000-000000000001"), SubCategoryName = "Defibrilatörler", CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"), CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
                new SubCategory { Id = Guid.Parse("0f111111-0000-0000-0000-000000000002"), SubCategoryName = "Enjeksiyon ve İğne", CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222"), CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
                new SubCategory { Id = Guid.Parse("0f111111-0000-0000-0000-000000000003"), SubCategoryName = "Otoklav Sarfı", CategoryId = Guid.Parse("33333333-3333-3333-3333-333333333333"), CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
                new SubCategory { Id = Guid.Parse("0f111111-0000-0000-0000-000000000004"), SubCategoryName = "Mikrobiyoloji Sarfı", CategoryId = Guid.Parse("44444444-4444-4444-4444-444444444444"), CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
                new SubCategory { Id = Guid.Parse("0f111111-0000-0000-0000-000000000005"), SubCategoryName = "Antibiyotik ve Serumlar", CategoryId = Guid.Parse("55555555-5555-5555-5555-555555555555"), CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
                new SubCategory { Id = Guid.Parse("0f111111-0000-0000-0000-000000000006"), SubCategoryName = "Röntgen Sarfı", CategoryId = Guid.Parse("66666666-6666-6666-6666-666666666666"), CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
                new SubCategory { Id = Guid.Parse("0f111111-0000-0000-0000-000000000007"), SubCategoryName = "Alçı ve Atel", CategoryId = Guid.Parse("77777777-7777-7777-7777-777777777777"), CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
                new SubCategory { Id = Guid.Parse("0f111111-0000-0000-0000-000000000008"), SubCategoryName = "Anestezi Maskeleri", CategoryId = Guid.Parse("88888888-8888-8888-8888-888888888888"), CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
                new SubCategory { Id = Guid.Parse("0f111111-0000-0000-0000-000000000009"), SubCategoryName = "Ventilatör Sarfı", CategoryId = Guid.Parse("99999999-9999-9999-9999-999999999999"), CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
                new SubCategory { Id = Guid.Parse("0f111111-0000-0000-0000-000000000010"), SubCategoryName = "Diş Üniti Sarfı", CategoryId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active }
            );
        }
    }
}
