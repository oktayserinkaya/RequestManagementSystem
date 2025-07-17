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
    public class SubCategorySeedData : IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.HasData(
                new SubCategory
                {
                    Id = Guid.Parse("0f111111-0000-0000-0000-0000000000ab"),
                    SubCategoryName = "Tabletler",
                    CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"), // Bilgisayarlar
                    CreatedDate = DateTime.Parse("2025-07-01"),
                    Status = Status.Active
                },
                new SubCategory
                {
                    Id = Guid.Parse("0f111111-0000-0000-0000-0000000000ac"),
                    SubCategoryName = "Web Kameraları",
                    CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // Çevre Birimleri
                    CreatedDate = DateTime.Parse("2025-07-01"),
                    Status = Status.Active
                },
                new SubCategory
                {
                    Id = Guid.Parse("0f111111-0000-0000-0000-0000000000ad"),
                    SubCategoryName = "Switch & Hub",
                    CategoryId = Guid.Parse("33333333-3333-3333-3333-333333333333"), // Ağ Cihazları
                    CreatedDate = DateTime.Parse("2025-07-01"),
                    Status = Status.Active
                },
                new SubCategory
                {
                    Id = Guid.Parse("0f111111-0000-0000-0000-0000000000ae"),
                    SubCategoryName = "Antivirüs Yazılımları",
                    CategoryId = Guid.Parse("44444444-4444-4444-4444-444444444444"), // Güvenlik
                    CreatedDate = DateTime.Parse("2025-07-01"),
                    Status = Status.Active
                }
            );
        }
    }



}
