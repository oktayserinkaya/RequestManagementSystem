using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class UserRoleSeedData : IEntityTypeConfiguration<IdentityUserRole<Guid>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
    {
        builder.HasKey(ur => new { ur.UserId, ur.RoleId }); // Composite primary key tanımı

        builder.HasData(
            new IdentityUserRole<Guid>
            {
                UserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // admin
                RoleId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa")  // Admin
            },
            new IdentityUserRole<Guid>
            {
                UserId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // Ahmet Yılmaz
                RoleId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb")  // TalepOluşturanBirim
            },
            new IdentityUserRole<Guid>
            {
                UserId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), // Elif Kara
                RoleId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc")  // İhtiyaçTespitKomisyonu
            },
            new IdentityUserRole<Guid>
            {
                UserId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), // Mehmet Demir
                RoleId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd")  // SatınAlmaBirimi
            },
            new IdentityUserRole<Guid>
            {
                UserId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), // Zeynep Şahin
                RoleId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee")  // DepoBirimi
            },

            new IdentityUserRole<Guid>
            {
                UserId = Guid.Parse("99999999-9999-9999-9999-999999999999"), // Fatma Öz
                RoleId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff")  // OdemeBirimi
            }
        );
    }
}