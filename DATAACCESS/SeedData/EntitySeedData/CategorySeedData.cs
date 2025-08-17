using System;
using CORE.Entities.Concrete;
using CORE.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DATAACCESS.SeedData.EntitySeedData
{
    public class CategorySeedData : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData
            (
                new Category { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), CategoryName = "Tıbbi Cihazlar", CreatedDate = DateTime.Parse("2024-01-01T00:00:00"), Status = Status.Active },
                new Category { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), CategoryName = "Sarf Malzemeleri", CreatedDate = DateTime.Parse("2024-01-01T00:00:00"), Status = Status.Active },
                new Category { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), CategoryName = "Sterilizasyon ve Hijyen", CreatedDate = DateTime.Parse("2024-01-01T00:00:00"), Status = Status.Active },
                new Category { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), CategoryName = "Laboratuvar", CreatedDate = DateTime.Parse("2024-01-01T00:00:00"), Status = Status.Active },
                new Category { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), CategoryName = "İlaç ve Serum", CreatedDate = DateTime.Parse("2024-01-01T00:00:00"), Status = Status.Active },
                new Category { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), CategoryName = "Radyoloji ve Görüntüleme", CreatedDate = DateTime.Parse("2024-01-01T00:00:00"), Status = Status.Active },
                new Category { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), CategoryName = "Ortopedi", CreatedDate = DateTime.Parse("2024-01-01T00:00:00"), Status = Status.Active },
                new Category { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), CategoryName = "Anestezi", CreatedDate = DateTime.Parse("2024-01-01T00:00:00"), Status = Status.Active },
                new Category { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), CategoryName = "Yoğun Bakım", CreatedDate = DateTime.Parse("2024-01-01T00:00:00"), Status = Status.Active },
                new Category { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), CategoryName = "Diş Hekimliği", CreatedDate = DateTime.Parse("2024-01-01T00:00:00"), Status = Status.Active }
            );
        }
    }
}
