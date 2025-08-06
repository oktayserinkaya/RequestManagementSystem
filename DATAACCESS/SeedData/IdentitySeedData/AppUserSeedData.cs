using CORE.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

public class AppUserSeedData : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        var hasher = new PasswordHasher<AppUser>();

        var users = new[]
        {
            new AppUser
            {
                Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "Sistem",
                LastName = "Yöneticisi",
                SecurityStamp = Guid.NewGuid().ToString("D"),
            },
            new AppUser
            {
                Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserName = "ahmetyilmaz",
                NormalizedUserName = "AHMETYILMAZ",
                Email = "ahmet.yilmaz@example.com",
                NormalizedEmail = "AHMET.YILMAZ@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "Ahmet",
                LastName = "Yılmaz",
                SecurityStamp = Guid.NewGuid().ToString("D"),
            },
            new AppUser
            {
                Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserName = "elifkara",
                NormalizedUserName = "ELIFKARA",
                Email = "elif.kara@example.com",
                NormalizedEmail = "ELIF.KARA@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "Elif",
                LastName = "Kara",
                SecurityStamp = Guid.NewGuid().ToString("D"),
            },
            new AppUser
            {
                Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserName = "mehmetdemir",
                NormalizedUserName = "MEHMETDEMIR",
                Email = "mehmet.demir@example.com",
                NormalizedEmail = "MEHMET.DEMIR@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "Mehmet",
                LastName = "Demir",
                SecurityStamp = Guid.NewGuid().ToString("D"),
            },
            new AppUser
            {
                Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserName = "zeynepsahin",
                NormalizedUserName = "ZEYNEPSAHIN",
                Email = "zeynep.sahin@example.com",
                NormalizedEmail = "ZEYNEP.SAHIN@EXAMPLE.COM",
                EmailConfirmed = true,
                FirstName = "Zeynep",
                LastName = "Şahin",
                SecurityStamp = Guid.NewGuid().ToString("D"),
            }
        };

        builder.HasData(users);
    }
}
