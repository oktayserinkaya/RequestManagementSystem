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
    public class CategorySeedData : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData
                (
                new Category
                {
                    Id = Guid.Parse("c1adac89-0156-4bed-b548-5270b5ebaf57"),
                    CategoryName = "Elektronik",
                    CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
                    Status = Status.Active
                },
    new Category
    {
        Id = Guid.Parse("491275e2-83a2-4ac0-a0fd-e40dd5d1876b"),
        CategoryName = "Ofis Malzemeleri",
        CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
        Status = Status.Active
    },
    new Category
    {
        Id = Guid.Parse("e2dd68f4-f793-4bfb-b8be-eb2c9e8c5e93"),
        CategoryName = "Temizlik Ürünleri",
        CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
        Status = Status.Active
    },
    new Category
    {
        Id = Guid.Parse("764ec7a2-3297-4088-8194-ad11113aa6ac"),
        CategoryName = "Gıda ve İçecek",
        CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
        Status = Status.Active
    },
    new Category
    {
        Id = Guid.Parse("f44475a4-dbeb-4392-ab47-59e48a5dd4ff"),
        CategoryName = "Kırtasiye",
        CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
        Status = Status.Active
    },
    new Category
    {
        Id = Guid.Parse("7b68dbf2-43be-4983-bcb3-b2b9705acf91"),
        CategoryName = "Mobilya",
        CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
        Status = Status.Active
    },
    new Category
    {
        Id = Guid.Parse("c3085fe5-86e4-4467-add2-ff40c50f056b"),
        CategoryName = "Yazılım Lisansları",
        CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
        Status = Status.Active
    },
    new Category
    {
        Id = Guid.Parse("f0621b68-9b1a-4835-84cc-0375bbb4cc60"),
        CategoryName = "Giyim ve Tekstil",
        CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
        Status = Status.Active
    },
    new Category
    {
        Id = Guid.Parse("67ae77e9-700b-4f06-bd9e-75e041f5aa91"),
        CategoryName = "İnşaat ve Tadilat",
        CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
        Status = Status.Active
    },
    new Category
    {
        Id = Guid.Parse("34916f95-803e-4340-8f44-0f3083ecb6de"),
        CategoryName = "Medikal Malzemeler",
        CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
        Status = Status.Active
    }
                );
        }
    }
}
