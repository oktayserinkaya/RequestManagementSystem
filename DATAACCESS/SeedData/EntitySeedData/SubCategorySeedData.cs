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
                        Id = Guid.Parse("c0000000-0000-0000-0000-000000000001"),
                        SubCategoryName = "Laptoplar",
                        CategoryId = Guid.Parse("cat00000-0000-0000-0000-000000000001"),
                        CreatedDate = DateTime.Parse("2025-07-01"),
                        UpdatedDate = null,
                        DeletedDate = null,
                        Status = Status.Active
                    },
    new SubCategory
    {
        Id = Guid.Parse("c0000000-0000-0000-0000-000000000002"),
        SubCategoryName = "Fare ve Klavye",
        CategoryId = Guid.Parse("cat00000-0000-0000-0000-000000000002"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new SubCategory
    {
        Id = Guid.Parse("c0000000-0000-0000-0000-000000000003"),
        SubCategoryName = "Monitörler",
        CategoryId = Guid.Parse("cat00000-0000-0000-0000-000000000001"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new SubCategory
    {
        Id = Guid.Parse("c0000000-0000-0000-0000-000000000004"),
        SubCategoryName = "Yazıcılar",
        CategoryId = Guid.Parse("cat00000-0000-0000-0000-000000000003"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new SubCategory
    {
        Id = Guid.Parse("c0000000-0000-0000-0000-000000000005"),
        SubCategoryName = "Ağ Donanımları",
        CategoryId = Guid.Parse("cat00000-0000-0000-0000-000000000004"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new SubCategory
    {
        Id = Guid.Parse("c0000000-0000-0000-0000-000000000006"),
        SubCategoryName = "Depolama Aygıtları",
        CategoryId = Guid.Parse("cat00000-0000-0000-0000-000000000004"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new SubCategory
    {
        Id = Guid.Parse("c0000000-0000-0000-0000-000000000007"),
        SubCategoryName = "Güvenlik Kameraları",
        CategoryId = Guid.Parse("cat00000-0000-0000-0000-000000000005"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new SubCategory
    {
        Id = Guid.Parse("c0000000-0000-0000-0000-000000000008"),
        SubCategoryName = "Ofis Bilgisayarları",
        CategoryId = Guid.Parse("cat00000-0000-0000-0000-000000000001"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new SubCategory
    {
        Id = Guid.Parse("c0000000-0000-0000-0000-000000000009"),
        SubCategoryName = "Gaming Aksesuarlar",
        CategoryId = Guid.Parse("cat00000-0000-0000-0000-000000000002"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new SubCategory
    {
        Id = Guid.Parse("c0000000-0000-0000-0000-000000000010"),
        SubCategoryName = "Yazılım Lisansları",
        CategoryId = Guid.Parse("cat00000-0000-0000-0000-000000000005"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    }
            );
        }
    }
}
