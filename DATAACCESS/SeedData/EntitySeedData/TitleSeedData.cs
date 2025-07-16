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
    public class TitleSeedData : IEntityTypeConfiguration<Title>
    {
        public void Configure(EntityTypeBuilder<Title> builder)
        {
            builder.HasData(
                // Önceki 10 kayıt burada kalacak, yeni 4 kayıt aşağıda eklenmiştir

                new Title
                {
                    Id = Guid.Parse("f0000011-aaaa-bbbb-cccc-0000000000ab"),
                    TitleName = "Finans Uzmanı",
                    CreatedDate = DateTime.Parse("2025-07-01"),
                    Status = Status.Active
                },
                new Title
                {
                    Id = Guid.Parse("f0000012-aaaa-bbbb-cccc-0000000000ac"),
                    TitleName = "Kalite Kontrol Sorumlusu",
                    CreatedDate = DateTime.Parse("2025-07-01"),
                    Status = Status.Active
                },
                new Title
                {
                    Id = Guid.Parse("f0000013-aaaa-bbbb-cccc-0000000000ad"),
                    TitleName = "Ar-Ge Mühendisi",
                    CreatedDate = DateTime.Parse("2025-07-01"),
                    Status = Status.Active
                },
                new Title
                {
                    Id = Guid.Parse("f0000014-aaaa-bbbb-cccc-0000000000ae"),
                    TitleName = "Depo Görevlisi",
                    CreatedDate = DateTime.Parse("2025-07-01"),
                    Status = Status.Active
                }
            );
        }
    }


}
