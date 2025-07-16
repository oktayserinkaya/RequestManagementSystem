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
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    CategoryName = "Elektronik",
                    CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
                    Status = Status.Active
                },
                new Category
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    CategoryName = "Ofis Malzemeleri",
                    CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
                    Status = Status.Active
                },
                new Category
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    CategoryName = "Temizlik Ürünleri",
                    CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
                    Status = Status.Active
                },
                new Category
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    CategoryName = "Mobilya",
                    CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
                    Status = Status.Active
                }
            );
        }
    }


}
