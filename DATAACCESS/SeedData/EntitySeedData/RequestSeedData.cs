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
    public class RequestSeedData : IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.HasData(
                new Request
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-0000000000ab"),
                    RequestDate = DateTime.Parse("2025-06-26"),
                    SpecialProductName = "Projeksiyon Cihazı",
                    Amount = 1,
                    ProductFeaturesFilePath = null,
                    ProductFeatures = "Full HD, HDMI destekli",
                    CommissionNote = "Toplantı odası için",
                    IsApproved = false,
                    EmployeeId = Guid.Parse("3e39b574-5a2c-4703-8c28-5ac11dc703d1"), // Ahmet Yılmaz
                    ProductId = Guid.Parse("11111111-aaaa-bbbb-cccc-111111111111"), // Canon EOS 2000D Kamera
                    CreatedDate = DateTime.Parse("2025-06-26"),
                    UpdatedDate = null,
                    DeletedDate = null,
                    Status = Status.Active
                },
                new Request
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-0000000000ac"),
                    RequestDate = DateTime.Parse("2025-06-27"),
                    SpecialProductName = null,
                    Amount = 2,
                    ProductFeaturesFilePath = null,
                    ProductFeatures = null,
                    CommissionNote = "Bilgi İşlem birimi için",
                    IsApproved = true,
                    EmployeeId = Guid.Parse("6177b7cc-68bc-4ae8-8020-2f7334a3bb0e"), // Elif Kara
                    ProductId = Guid.Parse("22222222-aaaa-bbbb-cccc-222222222222"), // Apple Magic Keyboard
                    CreatedDate = DateTime.Parse("2025-06-27"),
                    UpdatedDate = null,
                    DeletedDate = null,
                    Status = Status.Active
                },
                new Request
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-0000000000ad"),
                    RequestDate = DateTime.Parse("2025-06-28"),
                    SpecialProductName = null,
                    Amount = 1,
                    ProductFeaturesFilePath = null,
                    ProductFeatures = null,
                    CommissionNote = "Yönetici kullanımı",
                    IsApproved = true,
                    EmployeeId = Guid.Parse("cf54b94f-51fa-4b70-a9a6-3a53a1df4f1b"), // Mehmet Demir
                    ProductId = Guid.Parse("33333333-aaaa-bbbb-cccc-333333333333"), // Acer Nitro 5 Gaming Laptop
                    CreatedDate = DateTime.Parse("2025-06-28"),
                    UpdatedDate = null,
                    DeletedDate = null,
                    Status = Status.Active
                },
                new Request
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-0000000000ae"),
                    RequestDate = DateTime.Parse("2025-06-30"),
                    SpecialProductName = null,
                    Amount = 4,
                    ProductFeaturesFilePath = null,
                    ProductFeatures = null,
                    CommissionNote = "Etkinlik alanı için ses sistemi",
                    IsApproved = false,
                    EmployeeId = Guid.Parse("099e365b-2a0c-48a0-91cc-3eecc11215ed"), // Zeynep Şahin
                    ProductId = Guid.Parse("44444444-aaaa-bbbb-cccc-444444444444"), // JBL Bluetooth Hoparlör
                    CreatedDate = DateTime.Parse("2025-06-30"),
                    UpdatedDate = null,
                    DeletedDate = null,
                    Status = Status.Active
                }
            );
        }
    }

}
