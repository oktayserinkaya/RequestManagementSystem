using System;
using CORE.Entities.Concrete;
using CORE.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TitleSeedData : IEntityTypeConfiguration<Title>
{
    public void Configure(EntityTypeBuilder<Title> builder)
    {
        builder.HasData(
            new Title { Id = Guid.Parse("f0000011-aaaa-bbbb-cccc-0000000000ab"), TitleName = "Uzman Hekim", CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
            new Title { Id = Guid.Parse("f0000012-aaaa-bbbb-cccc-0000000000ac"), TitleName = "Hemşire", CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
            new Title { Id = Guid.Parse("f0000013-aaaa-bbbb-cccc-0000000000ad"), TitleName = "Eczacı", CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
            new Title { Id = Guid.Parse("f0000014-aaaa-bbbb-cccc-0000000000ae"), TitleName = "Radyoloji Teknisyeni", CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
            new Title { Id = Guid.Parse("f0000015-aaaa-bbbb-cccc-0000000000af"), TitleName = "Laboratuvar Teknisyeni", CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
            new Title { Id = Guid.Parse("f0000016-aaaa-bbbb-cccc-0000000000b0"), TitleName = "Biyomedikal Mühendisi", CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
            new Title { Id = Guid.Parse("f0000017-aaaa-bbbb-cccc-0000000000b1"), TitleName = "Anestezi Teknikeri", CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
            new Title { Id = Guid.Parse("f0000018-aaaa-bbbb-cccc-0000000000b2"), TitleName = "Ortopedi Teknikeri", CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
            new Title { Id = Guid.Parse("f0000019-aaaa-bbbb-cccc-0000000000b3"), TitleName = "Yoğun Bakım Hemşiresi", CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active },
            new Title { Id = Guid.Parse("f0000020-aaaa-bbbb-cccc-0000000000b4"), TitleName = "Diş Hekimi", CreatedDate = DateTime.Parse("2025-07-01"), Status = Status.Active }
        );
    }
}
