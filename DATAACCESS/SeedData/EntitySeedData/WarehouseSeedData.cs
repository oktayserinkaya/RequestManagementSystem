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
                    ProductId = Guid.Parse("a9fbdced-bc2a-4ad6-9ed3-11a54bdf0007"),
                    CategoryId = Guid.Parse("688b4c8a-1db3-41e8-9e4f-afec71eb2b81"), // Bilgisayarlar
                    SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-000000000008"), // Ofis Bilgisayarları
                    EmployeeId = Guid.Parse("80a124bb-b3c5-4bc2-9e3a-e70d889d0d4f"),
                    RequestId = Guid.Parse("10000000-0000-0000-0000-000000000007"),
                    DepartmentId = Guid.Parse("ce236df0-cf9e-4e6a-a30a-8c0661dc5a58"), // Ar-Ge
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
    ProductId = Guid.Parse("a9fbdced-bc2a-4ad6-9ed3-11a54bdf0009"),
    CategoryId = Guid.Parse("74e94f07-2064-46a4-a7b1-77e16b45df46"), // Güvenlik
    SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-000000000007"), // Güvenlik Kameraları
    EmployeeId = Guid.Parse("6bc19a61-8421-438f-b8f7-60de51b85955"),
    RequestId = Guid.Parse("10000000-0000-0000-0000-000000000008"),
    DepartmentId = Guid.Parse("aa58e7e1-cbc0-4859-b6f0-5375b6d10a7c"), // Kalite Kontrol
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
    ProductId = Guid.Parse("a9fbdced-bc2a-4ad6-9ed3-11a54bdf0010"),
    CategoryId = Guid.Parse("c011479a-fb3e-4c30-a2a5-bc5ee9c650ef"), // Ağ Cihazları
    SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-000000000006"), // Depolama Aygıtları
    EmployeeId = Guid.Parse("41e60754-30fd-4999-8b2b-b0d7c6c62347"),
    RequestId = Guid.Parse("10000000-0000-0000-0000-000000000009"),
    DepartmentId = Guid.Parse("c047423a-5ed4-4c6e-9bb4-5ddf02a6be38"), // Bakım ve Onarım
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
    ProductId = Guid.Parse("a9fbdced-bc2a-4ad6-9ed3-11a54bdf0008"),
    CategoryId = Guid.Parse("b92c7bc3-5dc4-4b12-b3de-f10ed70e32bb"), // Çevre Birimleri
    SubCategoryId = Guid.Parse("0f111111-0000-0000-0000-000000000009"), // Gaming Aksesuarlar
    EmployeeId = Guid.Parse("b1e6c330-dcd3-402e-b67a-999b65c296c3"),
    RequestId = Guid.Parse("10000000-0000-0000-0000-00000000000a"),
    DepartmentId = Guid.Parse("0c5fc66a-75f9-4d09-87e3-3c121cfa059b"), // Yönetim
    CreatedDate = DateTime.Parse("2025-07-01"),
    Status = Status.Active
}
            );
        }
    }



}
