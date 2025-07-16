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
                    CategoryId = Guid.Parse("688b4c8a-1db3-41e8-9e4f-afec71eb2b81"), // Bilgisayarlar
                    CreatedDate = DateTime.Parse("2025-07-01"),
                    Status = Status.Active
                },
                new SubCategory
                {
                    Id = Guid.Parse("0f111111-0000-0000-0000-0000000000ac"),
                    SubCategoryName = "Web Kameraları",
                    CategoryId = Guid.Parse("b92c7bc3-5dc4-4b12-b3de-f10ed70e32bb"), // Çevre Birimleri
                    CreatedDate = DateTime.Parse("2025-07-01"),
                    Status = Status.Active
                },
                new SubCategory
                {
                    Id = Guid.Parse("0f111111-0000-0000-0000-0000000000ad"),
                    SubCategoryName = "Switch & Hub",
                    CategoryId = Guid.Parse("c011479a-fb3e-4c30-a2a5-bc5ee9c650ef"), // Ağ Cihazları
                    CreatedDate = DateTime.Parse("2025-07-01"),
                    Status = Status.Active
                },
                new SubCategory
                {
                    Id = Guid.Parse("0f111111-0000-0000-0000-0000000000ae"),
                    SubCategoryName = "Antivirüs Yazılımları",
                    CategoryId = Guid.Parse("74e94f07-2064-46a4-a7b1-77e16b45df46"), // Güvenlik
                    CreatedDate = DateTime.Parse("2025-07-01"),
                    Status = Status.Active
                }
            );
        }
    }



}
