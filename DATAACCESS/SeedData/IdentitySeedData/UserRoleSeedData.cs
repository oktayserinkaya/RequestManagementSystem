using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserRoleSeedData : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        builder.HasKey(ur => new { ur.UserId, ur.RoleId });

        builder.HasData(
            // Mevcutlar (aynen)
            new IdentityUserRole<Guid> { UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), RoleId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") }, // admin -> Admin
            new IdentityUserRole<Guid> { UserId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), RoleId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") }, // Ahmet -> TalepOluşturanBirim
            new IdentityUserRole<Guid> { UserId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), RoleId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc") }, // Elif -> IhtiyacTespitKomisyonu
            new IdentityUserRole<Guid> { UserId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), RoleId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd") }, // Mehmet -> SatinAlmaBirimi
            new IdentityUserRole<Guid> { UserId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), RoleId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee") }, // Zeynep -> DepoBirimi
            new IdentityUserRole<Guid> { UserId = Guid.Parse("99999999-9999-9999-9999-999999999999"), RoleId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff") }, // Fatma -> OdemeBirimi

            // Yeni eklenenler
            new IdentityUserRole<Guid> { UserId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), RoleId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff") }, // Ayşe -> OdemeBirimi
            new IdentityUserRole<Guid> { UserId = Guid.Parse("00000000-0000-0000-0000-00000000000a"), RoleId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") }, // Kerem -> TalepOluşturanBirim
            new IdentityUserRole<Guid> { UserId = Guid.Parse("00000000-0000-0000-0000-00000000000b"), RoleId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd") }, // Derya -> SatinAlmaBirimi
            new IdentityUserRole<Guid> { UserId = Guid.Parse("00000000-0000-0000-0000-00000000000c"), RoleId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc") }, // Burak -> IhtiyacTespitKomisyonu
            new IdentityUserRole<Guid> { UserId = Guid.Parse("00000000-0000-0000-0000-00000000000d"), RoleId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") }  // Selin -> TalepOluşturanBirim
        );
    }
}
