using CORE.Entities.Concrete;
using CORE.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EmployeeSeedData : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasData(
            new Employee
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                AppUserId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // Ahmet Yılmaz
                FirstName = "Ahmet",
                LastName = "Yılmaz",
                Email = "ahmet.yilmaz@example.com",
                TitleId = Guid.Parse("f0000011-aaaa-bbbb-cccc-0000000000ab"),
                DepartmentId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                ImagePath = null,
                CreatedDate = DateTime.Parse("2025-01-01"),
                Status = Status.Active
            },
            new Employee
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                AppUserId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), // Elif Kara
                FirstName = "Elif",
                LastName = "Kara",
                Email = "elif.kara@example.com",
                TitleId = Guid.Parse("f0000012-aaaa-bbbb-cccc-0000000000ac"),
                DepartmentId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                ImagePath = null,
                CreatedDate = DateTime.Parse("2025-01-01"),
                Status = Status.Active
            },
            new Employee
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                AppUserId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), // Mehmet Demir
                FirstName = "Mehmet",
                LastName = "Demir",
                Email = "mehmet.demir@example.com",
                TitleId = Guid.Parse("f0000013-aaaa-bbbb-cccc-0000000000ad"),
                DepartmentId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                ImagePath = null,
                CreatedDate = DateTime.Parse("2025-01-01"),
                Status = Status.Active
            },
            new Employee
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                AppUserId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), // Zeynep Şahin
                FirstName = "Zeynep",
                LastName = "Şahin",
                Email = "zeynep.sahin@example.com",
                TitleId = Guid.Parse("f0000014-aaaa-bbbb-cccc-0000000000ae"),
                DepartmentId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                ImagePath = null,
                CreatedDate = DateTime.Parse("2025-01-01"),
                Status = Status.Active
            }
        );
    }
}
