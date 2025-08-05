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
            (
                new Department
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    DepartmentName = "Bilgi Teknolojileri",
                    TitleId = Guid.Parse("f0000011-aaaa-bbbb-cccc-0000000000ab"),
                    CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
                    Status = Status.Active
                },
                new Department
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    DepartmentName = "Satın Alma",
                    TitleId = Guid.Parse("f0000012-aaaa-bbbb-cccc-0000000000ac"),
                    CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
                    Status = Status.Active
                },
                new Department
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    DepartmentName = "Muhasebe",
                    TitleId = Guid.Parse("f0000013-aaaa-bbbb-cccc-0000000000ad"),
                    CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
                    Status = Status.Active
                },
                new Department
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    DepartmentName = "İnsan Kaynakları",
                    TitleId = Guid.Parse("f0000014-aaaa-bbbb-cccc-0000000000ae"),
                    CreatedDate = DateTime.Parse("2024-01-01T00:00:00"),
                    Status = Status.Active
                }
            );
        }
    }

}
