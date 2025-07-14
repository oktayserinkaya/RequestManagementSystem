using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DATAACCESS.SeedData.EntitySeedData
{
    public class EmployeeSeedData : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasData(
                    new Employee
                    {
                        Id = Guid.NewGuid(),
                        AppUserId = Guid.NewGuid(),
                        FirstName = "Ahmet",
                        LastName = "Yılmaz",
                        Email = "ahmet.yilmaz@example.com",
                        TitleId = Guid.NewGuid(),
                        DepartmentId = Guid.NewGuid(),
                        ImagePath = null
                    },
    new Employee
    {
        Id = Guid.NewGuid(),
        AppUserId = Guid.NewGuid(),
        FirstName = "Elif",
        LastName = "Kara",
        Email = "elif.kara@example.com",
        TitleId = Guid.NewGuid(),
        DepartmentId = Guid.NewGuid(),
        ImagePath = null
    },
    new Employee
    {
        Id = Guid.NewGuid(),
        AppUserId = Guid.NewGuid(),
        FirstName = "Mehmet",
        LastName = "Demir",
        Email = "mehmet.demir@example.com",
        TitleId = Guid.NewGuid(),
        DepartmentId = Guid.NewGuid(),
        ImagePath = null
    },
    new Employee
    {
        Id = Guid.NewGuid(),
        AppUserId = Guid.NewGuid(),
        FirstName = "Zeynep",
        LastName = "Şahin",
        Email = "zeynep.sahin@example.com",
        TitleId = Guid.NewGuid(),
        DepartmentId = Guid.NewGuid(),
        ImagePath = null
    },
    new Employee
    {
        Id = Guid.NewGuid(),
        AppUserId = Guid.NewGuid(),
        FirstName = "Ali",
        LastName = "Çelik",
        Email = "ali.celik@example.com",
        TitleId = Guid.NewGuid(),
        DepartmentId = Guid.NewGuid(),
        ImagePath = null
    },
    new Employee
    {
        Id = Guid.NewGuid(),
        AppUserId = Guid.NewGuid(),
        FirstName = "Merve",
        LastName = "Koç",
        Email = "merve.koc@example.com",
        TitleId = Guid.NewGuid(),
        DepartmentId = Guid.NewGuid(),
        ImagePath = null
    },
    new Employee
    {
        Id = Guid.NewGuid(),
        AppUserId = Guid.NewGuid(),
        FirstName = "Emre",
        LastName = "Aydın",
        Email = "emre.aydin@example.com",
        TitleId = Guid.NewGuid(),
        DepartmentId = Guid.NewGuid(),
        ImagePath = null
    },
    new Employee
    {
        Id = Guid.NewGuid(),
        AppUserId = Guid.NewGuid(),
        FirstName = "Selin",
        LastName = "Aslan",
        Email = "selin.aslan@example.com",
        TitleId = Guid.NewGuid(),
        DepartmentId = Guid.NewGuid(),
        ImagePath = null
    },
    new Employee
    {
        Id = Guid.NewGuid(),
        AppUserId = Guid.NewGuid(),
        FirstName = "Burak",
        LastName = "Güneş",
        Email = "burak.gunes@example.com",
        TitleId = Guid.NewGuid(),
        DepartmentId = Guid.NewGuid(),
        ImagePath = null
    },
    new Employee
    {
        Id = Guid.NewGuid(),
        AppUserId = Guid.NewGuid(),
        FirstName = "Gamze",
        LastName = "Kurt",
        Email = "gamze.kurt@example.com",
        TitleId = Guid.NewGuid(),
        DepartmentId = Guid.NewGuid(),
        ImagePath = null
    }
            );
        }
    }
}
