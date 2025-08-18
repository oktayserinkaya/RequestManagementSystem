using System;
using System.Collections.Generic;
using CORE.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AppUserSeedData : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        var hasher = new PasswordHasher<AppUser>();

        var users = new List<AppUser>
        {
            // Admin (aynen)
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
                HasFirstPasswordChanged = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                PasswordHash = hasher.HashPassword(null!, "123")
            },

            // Mevcut kullanıcılar (aynen)
            new AppUser
            {
                Id = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                UserName = "ahmetyilmaz",
                NormalizedUserName = "AHMETYILMAZ",
                Email = "ahmet.yilmaz@hospital.local",
                NormalizedEmail = "AHMET.YILMAZ@HOSPITAL.LOCAL",
                EmailConfirmed = true,
                FirstName = "Ahmet",
                LastName = "Yılmaz",
                HasFirstPasswordChanged = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                PasswordHash = hasher.HashPassword(null!, "123")
            },
            new AppUser
            {
                Id = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                UserName = "elifkara",
                NormalizedUserName = "ELIFKARA",
                Email = "elif.kara@hospital.local",
                NormalizedEmail = "ELIF.KARA@HOSPITAL.LOCAL",
                EmailConfirmed = true,
                FirstName = "Elif",
                LastName = "Kara",
                HasFirstPasswordChanged = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                PasswordHash = hasher.HashPassword(null!, "123")
            },
            new AppUser
            {
                Id = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                UserName = "mehmetdemir",
                NormalizedUserName = "MEHMETDEMIR",
                Email = "mehmet.demir@hospital.local",
                NormalizedEmail = "MEHMET.DEMIR@HOSPITAL.LOCAL",
                EmailConfirmed = true,
                FirstName = "Mehmet",
                LastName = "Demir",
                HasFirstPasswordChanged = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                PasswordHash = hasher.HashPassword(null!, "123")
            },
            new AppUser
            {
                Id = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                UserName = "zeynepsahin",
                NormalizedUserName = "ZEYNEPSAHIN",
                Email = "zeynep.sahin@hospital.local",
                NormalizedEmail = "ZEYNEP.SAHIN@HOSPITAL.LOCAL",
                EmailConfirmed = true,
                FirstName = "Zeynep",
                LastName = "Şahin",
                HasFirstPasswordChanged = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                PasswordHash = hasher.HashPassword(null!, "123")
            },

            // (Mevcut ama istersen kalabilir) - kullanımı şart değil
            new AppUser
            {
                Id = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                UserName = "fatmaoz",
                NormalizedUserName = "FATMAOZ",
                Email = "fatma.oz@hospital.local",
                NormalizedEmail = "FATMA.OZ@HOSPITAL.LOCAL",
                EmailConfirmed = true,
                FirstName = "Fatma",
                LastName = "Öz",
                HasFirstPasswordChanged = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                PasswordHash = hasher.HashPassword(null!, "123")
            },

            // === EKSİK OLANLAR (Employee/AppUserId eşleşmeleri için) ===

            // ffffffff-....  -> Ayşe Akın (Eczane / Eczacı)
            new AppUser
            {
                Id = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                UserName = "ayseakin",
                NormalizedUserName = "AYSEAKIN",
                Email = "ayse.akin@hospital.local",
                NormalizedEmail = "AYSE.AKIN@HOSPITAL.LOCAL",
                EmailConfirmed = true,
                FirstName = "Ayşe",
                LastName = "Akın",
                HasFirstPasswordChanged = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                PasswordHash = hasher.HashPassword(null!, "123")
            },

            // 00000000-0000-0000-0000-00000000000a -> Kerem Acar (Biyomedikal)
            new AppUser
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000a"),
                UserName = "keremacar",
                NormalizedUserName = "KEREMACAR",
                Email = "kerem.acar@hospital.local",
                NormalizedEmail = "KEREM.ACAR@HOSPITAL.LOCAL",
                EmailConfirmed = true,
                FirstName = "Kerem",
                LastName = "Acar",
                HasFirstPasswordChanged = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                PasswordHash = hasher.HashPassword(null!, "123")
            },

            // 00000000-0000-0000-0000-00000000000b -> Derya Uslu (Ortopedi)
            new AppUser
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000b"),
                UserName = "deryauslu",
                NormalizedUserName = "DERYAUSLU",
                Email = "derya.uslu@hospital.local",
                NormalizedEmail = "DERYA.USLU@HOSPITAL.LOCAL",
                EmailConfirmed = true,
                FirstName = "Derya",
                LastName = "Uslu",
                HasFirstPasswordChanged = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                PasswordHash = hasher.HashPassword(null!, "123")
            },

            // 00000000-0000-0000-0000-00000000000c -> Burak Keskin (Anestezi)
            new AppUser
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000c"),
                UserName = "burakkeskin",
                NormalizedUserName = "BURAKKESKIN",
                Email = "burak.keskin@hospital.local",
                NormalizedEmail = "BURAK.KESKIN@HOSPITAL.LOCAL",
                EmailConfirmed = true,
                FirstName = "Burak",
                LastName = "Keskin",
                HasFirstPasswordChanged = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                PasswordHash = hasher.HashPassword(null!, "123")
            },

            // 00000000-0000-0000-0000-00000000000d -> Selin Koral (Diş Kliniği)
            new AppUser
            {
                Id = Guid.Parse("00000000-0000-0000-0000-00000000000d"),
                UserName = "selinkoral",
                NormalizedUserName = "SELINKORAL",
                Email = "selin.koral@hospital.local",
                NormalizedEmail = "SELIN.KORAL@HOSPITAL.LOCAL",
                EmailConfirmed = true,
                FirstName = "Selin",
                LastName = "Koral",
                HasFirstPasswordChanged = true,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                PasswordHash = hasher.HashPassword(null!, "123")
            }
        };

        builder.HasData(users);
    }
}
