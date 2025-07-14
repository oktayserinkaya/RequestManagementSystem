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
    public class TitleSeedData : IEntityTypeConfiguration<Title>
    {
        public void Configure(EntityTypeBuilder<Title> builder)
        {
            builder.HasData(
                    new Title
                    {
                        Id = Guid.Parse("t0000000-0000-0000-0000-000000000001"),
                        TitleName = "Genel Müdür",
                        CreatedDate = DateTime.Parse("2025-07-01"),
                        UpdatedDate = null,
                        DeletedDate = null,
                        Status = Status.Active
                    },
    new Title
    {
        Id = Guid.Parse("t0000000-0000-0000-0000-000000000002"),
        TitleName = "Müdür Yardımcısı",
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Title
    {
        Id = Guid.Parse("t0000000-0000-0000-0000-000000000003"),
        TitleName = "Departman Müdürü",
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Title
    {
        Id = Guid.Parse("t0000000-0000-0000-0000-000000000004"),
        TitleName = "Kıdemli Uzman",
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Title
    {
        Id = Guid.Parse("t0000000-0000-0000-0000-000000000005"),
        TitleName = "Uzman",
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Title
    {
        Id = Guid.Parse("t0000000-0000-0000-0000-000000000006"),
        TitleName = "Stajyer",
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Title
    {
        Id = Guid.Parse("t0000000-0000-0000-0000-000000000007"),
        TitleName = "İnsan Kaynakları Uzmanı",
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Title
    {
        Id = Guid.Parse("t0000000-0000-0000-0000-000000000008"),
        TitleName = "Satınalma Sorumlusu",
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Title
    {
        Id = Guid.Parse("t0000000-0000-0000-0000-000000000009"),
        TitleName = "Bilgi İşlem Uzmanı",
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Title
    {
        Id = Guid.Parse("t0000000-0000-0000-0000-000000000010"),
        TitleName = "Destek Personeli",
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    }
            );
        }
    }
}
