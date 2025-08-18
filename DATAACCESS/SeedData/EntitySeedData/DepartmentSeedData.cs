using System;
using CORE.Entities.Concrete;
using CORE.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DepartmentSeedData : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.HasData(
            new Department { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), DepartmentName = "Acil Servis", TitleId = Guid.Parse("f0000011-aaaa-bbbb-cccc-0000000000ab"), CreatedDate = DateTime.Parse("2024-01-01T00:00:00"), Status = Status.Active },
            new Department { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), DepartmentName = "Ameliyathane", TitleId = Guid.Parse("f0000017-aaaa-bbbb-cccc-0000000000b1"), CreatedDate = DateTime.Parse("2024-01-01T00:00:00"), Status = Status.Active },
            new Department { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), DepartmentName = "Yoğun Bakım", TitleId = Guid.Parse("f0000019-aaaa-bbbb-cccc-0000000000b3"), CreatedDate = DateTime.Parse("2024-01-01T00:00:00"), Status = Status.Active },
            new Department { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), DepartmentName = "Radyoloji", TitleId = Guid.Parse("f0000014-aaaa-bbbb-cccc-0000000000ae"), CreatedDate = DateTime.Parse("2024-01-01T00:00:00"), Status = Status.Active },
            new Department { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), DepartmentName = "Laboratuvar", TitleId = Guid.Parse("f0000015-aaaa-bbbb-cccc-0000000000af"), CreatedDate = DateTime.Parse("2024-01-01T00:00:00"), Status = Status.Active },
            new Department { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), DepartmentName = "Eczane", TitleId = Guid.Parse("f0000013-aaaa-bbbb-cccc-0000000000ad"), CreatedDate = DateTime.Parse("2024-01-01T00:00:00"), Status = Status.Active },
            new Department { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), DepartmentName = "Sterilizasyon (CSSD)", TitleId = Guid.Parse("f0000016-aaaa-bbbb-cccc-0000000000b0"), CreatedDate = DateTime.Parse("2024-01-01T00:00:00"), Status = Status.Active },
            new Department { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), DepartmentName = "Ortopedi", TitleId = Guid.Parse("f0000018-aaaa-bbbb-cccc-0000000000b2"), CreatedDate = DateTime.Parse("2024-01-01T00:00:00"), Status = Status.Active },
            new Department { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), DepartmentName = "Anestezi ve Reanimasyon", TitleId = Guid.Parse("f0000017-aaaa-bbbb-cccc-0000000000b1"), CreatedDate = DateTime.Parse("2024-01-01T00:00:00"), Status = Status.Active },
            new Department { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), DepartmentName = "Diş Kliniği", TitleId = Guid.Parse("f0000020-aaaa-bbbb-cccc-0000000000b4"), CreatedDate = DateTime.Parse("2024-01-01T00:00:00"), Status = Status.Active }
        );
    }
}
