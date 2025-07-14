using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Concrete;
using CORE.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DATAACCESS.SeedData.EntitySeedData
{
    public class DepartmentSeedData : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasData
                (new Department
                {
                    Id = Guid.Parse("b3c4a99a-63f1-4bfb-ae36-0d369f456001"),
                    DepartmentName = "Bilgi Teknolojileri",
                    CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
                    Status = Status.Active
                },
    new Department
    {
        Id = Guid.Parse("d1a7a12c-72ce-4f25-9f9a-91ac94d67412"),
        DepartmentName = "Satın Alma",
        CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
        Status = Status.Active
    },
    new Department
    {
        Id = Guid.Parse("4cb59a1f-d2c3-46ef-a802-21c9b2f58ae3"),
        DepartmentName = "Depo Yönetimi",
        CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
        Status = Status.Active
    },
    new Department
    {
        Id = Guid.Parse("a6c6bb4e-0fd2-4e82-ae1f-4826f27f0c7a"),
        DepartmentName = "Muhasebe",
        CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
        Status = Status.Active
    },
    new Department
    {
        Id = Guid.Parse("eda04aa4-dfb0-44f0-8be2-202c9dc4e676"),
        DepartmentName = "İnsan Kaynakları",
        CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
        Status = Status.Active
    },
    new Department
    {
        Id = Guid.Parse("f2133e78-dffd-4d70-9a07-bb963d00d2a5"),
        DepartmentName = "Hukuk",
        CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
        Status = Status.Active
    },
    new Department
    {
        Id = Guid.Parse("ce236df0-cf9e-4e6a-a30a-8c0661dc5a58"),
        DepartmentName = "Ar-Ge",
        CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
        Status = Status.Active
    },
    new Department
    {
        Id = Guid.Parse("aa58e7e1-cbc0-4859-b6f0-5375b6d10a7c"),
        DepartmentName = "Kalite Kontrol",
        CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
        Status = Status.Active
    },
    new Department
    {
        Id = Guid.Parse("c047423a-5ed4-4c6e-9bb4-5ddf02a6be38"),
        DepartmentName = "Bakım ve Onarım",
        CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
        Status = Status.Active
    },
    new Department
    {
        Id = Guid.Parse("0c5fc66a-75f9-4d09-87e3-3c121cfa059b"),
        DepartmentName = "Yönetim",
        CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
        Status = Status.Active
    }
                );

        }
    }
}
