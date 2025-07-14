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
                        Id = Guid.Parse("r0000000-0000-0000-0000-000000000001"),
                        RequestDate = DateTime.Parse("2025-06-01"),
                        SpecialProductName = null,
                        Amount = 2,
                        ProductFeaturesFilePath = null,
                        ProductFeatures = null,
                        CommissionNote = "Acil ihtiyaç",
                        IsApproved = false,
                        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000001"),
                        ProductId = Guid.Parse("p0000000-0000-0000-0000-000000000001"),
                        CreatedDate = DateTime.Parse("2025-06-01"),
                        UpdatedDate = null,
                        DeletedDate = null,
                        Status = Status.Active
                    },
    new Request
    {
        Id = Guid.Parse("r0000000-0000-0000-0000-000000000002"),
        RequestDate = DateTime.Parse("2025-06-05"),
        SpecialProductName = "Özel masaüstü PC",
        Amount = 1,
        ProductFeaturesFilePath = null,
        ProductFeatures = "16GB RAM, 1TB SSD",
        CommissionNote = null,
        IsApproved = true,
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000002"),
        ProductId = Guid.Parse("p0000000-0000-0000-0000-000000000002"),
        CreatedDate = DateTime.Parse("2025-06-05"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Request
    {
        Id = Guid.Parse("r0000000-0000-0000-0000-000000000003"),
        RequestDate = DateTime.Parse("2025-06-10"),
        SpecialProductName = null,
        Amount = 3,
        ProductFeaturesFilePath = null,
        ProductFeatures = null,
        CommissionNote = "Ofis için",
        IsApproved = false,
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000003"),
        ProductId = Guid.Parse("p0000000-0000-0000-0000-000000000003"),
        CreatedDate = DateTime.Parse("2025-06-10"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Request
    {
        Id = Guid.Parse("r0000000-0000-0000-0000-000000000004"),
        RequestDate = DateTime.Parse("2025-06-12"),
        SpecialProductName = null,
        Amount = 1,
        ProductFeaturesFilePath = null,
        ProductFeatures = null,
        CommissionNote = "İhale kapsamında",
        IsApproved = true,
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000004"),
        ProductId = Guid.Parse("p0000000-0000-0000-0000-000000000004"),
        CreatedDate = DateTime.Parse("2025-06-12"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Request
    {
        Id = Guid.Parse("r0000000-0000-0000-0000-000000000005"),
        RequestDate = DateTime.Parse("2025-06-15"),
        SpecialProductName = "Yüksek hızlı modem",
        Amount = 5,
        ProductFeaturesFilePath = null,
        ProductFeatures = "Dual Band, 5 GHz",
        CommissionNote = null,
        IsApproved = false,
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000005"),
        ProductId = Guid.Parse("p0000000-0000-0000-0000-000000000005"),
        CreatedDate = DateTime.Parse("2025-06-15"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Request
    {
        Id = Guid.Parse("r0000000-0000-0000-0000-000000000006"),
        RequestDate = DateTime.Parse("2025-06-18"),
        SpecialProductName = null,
        Amount = 10,
        ProductFeaturesFilePath = null,
        ProductFeatures = null,
        CommissionNote = "Okul için toplu alım",
        IsApproved = true,
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000006"),
        ProductId = Guid.Parse("p0000000-0000-0000-0000-000000000006"),
        CreatedDate = DateTime.Parse("2025-06-18"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Request
    {
        Id = Guid.Parse("r0000000-0000-0000-0000-000000000007"),
        RequestDate = DateTime.Parse("2025-06-20"),
        SpecialProductName = null,
        Amount = 2,
        ProductFeaturesFilePath = null,
        ProductFeatures = null,
        CommissionNote = null,
        IsApproved = false,
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000007"),
        ProductId = Guid.Parse("p0000000-0000-0000-0000-000000000007"),
        CreatedDate = DateTime.Parse("2025-06-20"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Request
    {
        Id = Guid.Parse("r0000000-0000-0000-0000-000000000008"),
        RequestDate = DateTime.Parse("2025-06-21"),
        SpecialProductName = "Güvenlik kamerası",
        Amount = 4,
        ProductFeaturesFilePath = null,
        ProductFeatures = "Gece görüşlü, 360 derece",
        CommissionNote = "Deneme amaçlı",
        IsApproved = true,
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000008"),
        ProductId = Guid.Parse("p0000000-0000-0000-0000-000000000009"),
        CreatedDate = DateTime.Parse("2025-06-21"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Request
    {
        Id = Guid.Parse("r0000000-0000-0000-0000-000000000009"),
        RequestDate = DateTime.Parse("2025-06-22"),
        SpecialProductName = null,
        Amount = 3,
        ProductFeaturesFilePath = null,
        ProductFeatures = null,
        CommissionNote = "Dış mekan kullanımı",
        IsApproved = false,
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000009"),
        ProductId = Guid.Parse("p0000000-0000-0000-0000-000000000010"),
        CreatedDate = DateTime.Parse("2025-06-22"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Request
    {
        Id = Guid.Parse("r0000000-0000-0000-0000-000000000010"),
        RequestDate = DateTime.Parse("2025-06-25"),
        SpecialProductName = null,
        Amount = 6,
        ProductFeaturesFilePath = null,
        ProductFeatures = null,
        CommissionNote = "Yıl sonu bütçe kullanımı",
        IsApproved = true,
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000010"),
        ProductId = Guid.Parse("p0000000-0000-0000-0000-000000000008"),
        CreatedDate = DateTime.Parse("2025-06-25"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    }
            );
        }
    }
}
