using System;
using CORE.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DATAACCESS.SeedData.IdentitySeedData
{
    public class AppRoleSeedData : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasData
            (
                new AppRole { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), Name = "Admin", NormalizedName = "ADMIN" },
                new AppRole { Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), Name = "TalepOluşturanBirim", NormalizedName = "TALEPOLUSTURANBIRIM" },
                new AppRole { Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), Name = "IhtiyacTespitKomisyonu", NormalizedName = "IHTIYACTESPITKOMISYONU" },
                new AppRole { Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), Name = "SatinAlmaBirimi", NormalizedName = "SATINALMABIRIMI" },
                new AppRole { Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), Name = "DepoBirimi", NormalizedName = "DEPOBIRIMI" },
                new AppRole { Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), Name = "OdemeBirimi", NormalizedName = "ODEMEBIRIMI" }
            );
        }
    }
}
