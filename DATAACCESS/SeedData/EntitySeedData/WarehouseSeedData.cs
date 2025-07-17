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
    public class WarehouseSeedData : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasData(
                new Warehouse
                {
                    Id = Guid.Parse("1a30dfe7-0cf1-41c7-bb85-eeb0a0cf1001"),
                    StockOutAmount = 3,
                    StockInAmount = 8,
                    GeneralStockAmount = 5,
                    WaybillNumber = "WB-1007",
                    WaybillPrice = "18400",
                    ProductId = Guid.Parse("11111111-aaaa-bbbb-cccc-111111111111"),
                    CategoryId = Guid.Parse("11111111-1111-1111-1111-111111111111"), // Bilgisayarlar
                    SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-0000000000ab"), // Ofis Bilgisayarları
                    EmployeeId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    RequestId = Guid.Parse("10000000-0000-0000-0000-0000000000ab"),
                    DepartmentId = Guid.Parse("11111111-1111-1111-1111-111111111111"), // Ar-Ge
                    CreatedDate = DateTime.Parse("2025-07-01"),
                    Status = Status.Active
                },
new Warehouse
{
    Id = Guid.Parse("94d41b56-9634-49b3-abc5-75fce7f41002"),
    StockOutAmount = 1,
    StockInAmount = 6,
    GeneralStockAmount = 5,
    WaybillNumber = "WB-1008",
    WaybillPrice = "9000",
    ProductId = Guid.Parse("22222222-aaaa-bbbb-cccc-222222222222"),
    CategoryId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // Güvenlik
    SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-0000000000ac"), // Güvenlik Kameraları
    EmployeeId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
    RequestId = Guid.Parse("10000000-0000-0000-0000-0000000000ac"),
    DepartmentId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // Kalite Kontrol
    CreatedDate = DateTime.Parse("2025-07-01"),
    Status = Status.Active
},
new Warehouse
{
    Id = Guid.Parse("e8911d5e-9ef6-465a-988c-bc6bcbb11003"),
    StockOutAmount = 0,
    StockInAmount = 5,
    GeneralStockAmount = 5,
    WaybillNumber = "WB-1009",
    WaybillPrice = "6700",
    ProductId = Guid.Parse("33333333-aaaa-bbbb-cccc-333333333333"),
    CategoryId = Guid.Parse("33333333-3333-3333-3333-333333333333"), // Ağ Cihazları
    SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-0000000000ad"), // Depolama Aygıtları
    EmployeeId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
    RequestId = Guid.Parse("10000000-0000-0000-0000-0000000000ad"),
    DepartmentId = Guid.Parse("33333333-3333-3333-3333-333333333333"), // Bakım ve Onarım
    CreatedDate = DateTime.Parse("2025-07-01"),
    Status = Status.Active
},
new Warehouse
{
    Id = Guid.Parse("a7074ea5-3c5d-45c7-9872-4de0a2f21004"),
    StockOutAmount = 2,
    StockInAmount = 10,
    GeneralStockAmount = 8,
    WaybillNumber = "WB-1010",
    WaybillPrice = "10400",
    ProductId = Guid.Parse("44444444-aaaa-bbbb-cccc-444444444444"),
    CategoryId = Guid.Parse("44444444-4444-4444-4444-444444444444"), // Çevre Birimleri
    SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-0000000000ae"), // Gaming Aksesuarlar
    EmployeeId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
    RequestId = Guid.Parse("10000000-0000-0000-0000-0000000000ae"),
    DepartmentId = Guid.Parse("44444444-4444-4444-4444-444444444444"), // Yönetim
    CreatedDate = DateTime.Parse("2025-07-01"),
    Status = Status.Active
}
            );
        }
    }



}
