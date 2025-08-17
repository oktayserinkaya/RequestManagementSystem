using System;
using CORE.Entities.Concrete;
using CORE.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EmployeeSeedData : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasData(
            new Employee { Id = Guid.Parse("e1111111-1111-1111-1111-111111111111"), AppUserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), FirstName = "Ahmet", LastName = "Yılmaz", Email = "ahmet.yilmaz@hospital.local", TitleId = Guid.Parse("f0000011-aaaa-bbbb-cccc-0000000000ab"), DepartmentId = Guid.Parse("11111111-1111-1111-1111-111111111111"), ImagePath = null, CreatedDate = DateTime.Parse("2025-01-01"), Status = Status.Active },
            new Employee { Id = Guid.Parse("e2222222-2222-2222-2222-222222222222"), AppUserId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), FirstName = "Elif", LastName = "Kara", Email = "elif.kara@hospital.local", TitleId = Guid.Parse("f0000017-aaaa-bbbb-cccc-0000000000b1"), DepartmentId = Guid.Parse("22222222-2222-2222-2222-222222222222"), ImagePath = null, CreatedDate = DateTime.Parse("2025-01-01"), Status = Status.Active },
            new Employee { Id = Guid.Parse("e3333333-3333-3333-3333-333333333333"), AppUserId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), FirstName = "Mehmet", LastName = "Demir", Email = "mehmet.demir@hospital.local", TitleId = Guid.Parse("f0000019-aaaa-bbbb-cccc-0000000000b3"), DepartmentId = Guid.Parse("33333333-3333-3333-3333-333333333333"), ImagePath = null, CreatedDate = DateTime.Parse("2025-01-01"), Status = Status.Active },
            new Employee { Id = Guid.Parse("e4444444-4444-4444-4444-444444444444"), AppUserId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), FirstName = "Zeynep", LastName = "Şahin", Email = "zeynep.sahin@hospital.local", TitleId = Guid.Parse("f0000014-aaaa-bbbb-cccc-0000000000ae"), DepartmentId = Guid.Parse("44444444-4444-4444-4444-444444444444"), ImagePath = null, CreatedDate = DateTime.Parse("2025-01-01"), Status = Status.Active },
            new Employee { Id = Guid.Parse("e5555555-5555-5555-5555-555555555555"), AppUserId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), FirstName = "Can", LastName = "Yıldız", Email = "can.yildiz@hospital.local", TitleId = Guid.Parse("f0000015-aaaa-bbbb-cccc-0000000000af"), DepartmentId = Guid.Parse("55555555-5555-5555-5555-555555555555"), ImagePath = null, CreatedDate = DateTime.Parse("2025-01-01"), Status = Status.Active },
            new Employee { Id = Guid.Parse("e6666666-6666-6666-6666-666666666666"), AppUserId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), FirstName = "Ayşe", LastName = "Akın", Email = "ayse.akin@hospital.local", TitleId = Guid.Parse("f0000013-aaaa-bbbb-cccc-0000000000ad"), DepartmentId = Guid.Parse("66666666-6666-6666-6666-666666666666"), ImagePath = null, CreatedDate = DateTime.Parse("2025-01-01"), Status = Status.Active },
            new Employee { Id = Guid.Parse("e7777777-7777-7777-7777-777777777777"), AppUserId = Guid.Parse("00000000-0000-0000-0000-00000000000a"), FirstName = "Kerem", LastName = "Acar", Email = "kerem.acar@hospital.local", TitleId = Guid.Parse("f0000016-aaaa-bbbb-cccc-0000000000b0"), DepartmentId = Guid.Parse("77777777-7777-7777-7777-777777777777"), ImagePath = null, CreatedDate = DateTime.Parse("2025-01-01"), Status = Status.Active },
            new Employee { Id = Guid.Parse("e8888888-8888-8888-8888-888888888888"), AppUserId = Guid.Parse("00000000-0000-0000-0000-00000000000b"), FirstName = "Derya", LastName = "Uslu", Email = "derya.uslu@hospital.local", TitleId = Guid.Parse("f0000018-aaaa-bbbb-cccc-0000000000b2"), DepartmentId = Guid.Parse("88888888-8888-8888-8888-888888888888"), ImagePath = null, CreatedDate = DateTime.Parse("2025-01-01"), Status = Status.Active },
            new Employee { Id = Guid.Parse("e9999999-9999-9999-9999-999999999999"), AppUserId = Guid.Parse("00000000-0000-0000-0000-00000000000c"), FirstName = "Burak", LastName = "Keskin", Email = "burak.keskin@hospital.local", TitleId = Guid.Parse("f0000017-aaaa-bbbb-cccc-0000000000b1"), DepartmentId = Guid.Parse("99999999-9999-9999-9999-999999999999"), ImagePath = null, CreatedDate = DateTime.Parse("2025-01-01"), Status = Status.Active },
            new Employee { Id = Guid.Parse("eaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), AppUserId = Guid.Parse("00000000-0000-0000-0000-00000000000d"), FirstName = "Selin", LastName = "Koral", Email = "selin.koral@hospital.local", TitleId = Guid.Parse("f0000020-aaaa-bbbb-cccc-0000000000b4"), DepartmentId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), ImagePath = null, CreatedDate = DateTime.Parse("2025-01-01"), Status = Status.Active }
        );
    }
}
