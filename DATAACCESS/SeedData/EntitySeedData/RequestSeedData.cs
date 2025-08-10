using CORE.Entities.Concrete;
using CORE.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class RequestSeedData : IEntityTypeConfiguration<Request>
{
    public void Configure(EntityTypeBuilder<Request> builder)
    {
        builder.HasData(
            new
            {
                Id = Guid.Parse("10000000-0000-0000-0000-0000000000ab"),
                RequestDate = DateTime.Parse("2025-06-26"),
                SpecialProductName = "Projeksiyon Cihazı",
                Amount = 1.0m,
                ProductFeatures = "Full HD, HDMI destekli",
                CommissionNote = "Toplantı odası için",
                IsApproved = false,
                EmployeeId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                AppUserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                ProductId = Guid.Parse("11111111-aaaa-bbbb-cccc-111111111111"),
                TitleId = Guid.Parse("f0000011-aaaa-bbbb-cccc-0000000000ab"),
                DepartmentId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                CreatedDate = DateTime.Parse("2025-06-26"),
                Status = Status.Active
            },
            new
            {
                Id = Guid.Parse("10000000-0000-0000-0000-0000000000ac"),
                RequestDate = DateTime.Parse("2025-06-27"),
                SpecialProductName = "Projeksiyon Cihazı", // Açıkça string olarak null atama
                Amount = 2.0m,
                ProductFeatures = "Full HD, HDMI destekli", // Açıkça string olarak null atama
                CommissionNote = "Bilgi İşlem birimi için",
                IsApproved = true,
                EmployeeId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                AppUserId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                ProductId = Guid.Parse("22222222-aaaa-bbbb-cccc-222222222222"),
                TitleId = Guid.Parse("f0000012-aaaa-bbbb-cccc-0000000000ac"),
                DepartmentId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                CreatedDate = DateTime.Parse("2025-06-27"),
                Status = Status.Active
            },
            new
            {
                Id = Guid.Parse("10000000-0000-0000-0000-0000000000ad"),
                RequestDate = DateTime.Parse("2025-06-28"),
                SpecialProductName = "Full HD, HDMI destekli", // Açıkça string olarak null atama
                Amount = 1.0m,
                ProductFeatures = "Full HD, HDMI destekli", // Açıkça string olarak null atama
                CommissionNote = "Yönetici kullanımı",
                IsApproved = true,
                EmployeeId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                AppUserId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                ProductId = Guid.Parse("33333333-aaaa-bbbb-cccc-333333333333"),
                TitleId = Guid.Parse("f0000013-aaaa-bbbb-cccc-0000000000ad"),
                DepartmentId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                CreatedDate = DateTime.Parse("2025-06-28"),
                Status = Status.Active
            },
            new
            {
                Id = Guid.Parse("10000000-0000-0000-0000-0000000000ae"),
                RequestDate = DateTime.Parse("2025-06-30"),
                SpecialProductName = "Full HD, HDMI destekli", // Açıkça string olarak null atama
                Amount = 4.0m,
                ProductFeatures = "Full HD, HDMI destekli", // Açıkça string olarak null atama
                CommissionNote = "Etkinlik alanı için ses sistemi",
                IsApproved = false,
                EmployeeId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                AppUserId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                ProductId = Guid.Parse("44444444-aaaa-bbbb-cccc-444444444444"),
                TitleId = Guid.Parse("f0000014-aaaa-bbbb-cccc-0000000000ae"),
                DepartmentId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                CreatedDate = DateTime.Parse("2025-06-30"),
                Status = Status.Active
            }
        );
    }
}