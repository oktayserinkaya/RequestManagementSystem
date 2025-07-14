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
                        Id = Guid.Parse("w0000000-0000-0000-0000-000000000001"),
                        StockOutAmount = 2,
                        StockInAmount = 10,
                        GeneralStockAmount = 8,
                        WaybillNumber = "WB-1001",
                        WaybillPrice = "15000",
                        ProductId = Guid.Parse("p0000000-0000-0000-0000-000000000001"),
                        CategoryId = Guid.Parse("cat00000-0000-0000-0000-000000000001"),
                        SubCategoryId = Guid.Parse("c0000000-0000-0000-0000-000000000001"),
                        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000001"),
                        RequestId = Guid.Parse("r0000000-0000-0000-0000-000000000001"),
                        DepartmentId = Guid.Parse("d0000000-0000-0000-0000-000000000001"),
                        CreatedDate = DateTime.Parse("2025-07-01"),
                        UpdatedDate = null,
                        DeletedDate = null,
                        Status = Status.Active
                    },
    new Warehouse
    {
        Id = Guid.Parse("w0000000-0000-0000-0000-000000000002"),
        StockOutAmount = 1,
        StockInAmount = 5,
        GeneralStockAmount = 4,
        WaybillNumber = "WB-1002",
        WaybillPrice = "7200",
        ProductId = Guid.Parse("p0000000-0000-0000-0000-000000000002"),
        CategoryId = Guid.Parse("cat00000-0000-0000-0000-000000000002"),
        SubCategoryId = Guid.Parse("c0000000-0000-0000-0000-000000000002"),
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000002"),
        RequestId = Guid.Parse("r0000000-0000-0000-0000-000000000002"),
        DepartmentId = Guid.Parse("d0000000-0000-0000-0000-000000000002"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Warehouse
    {
        Id = Guid.Parse("w0000000-0000-0000-0000-000000000003"),
        StockOutAmount = 0,
        StockInAmount = 3,
        GeneralStockAmount = 3,
        WaybillNumber = "WB-1003",
        WaybillPrice = "9600",
        ProductId = Guid.Parse("p0000000-0000-0000-0000-000000000003"),
        CategoryId = Guid.Parse("cat00000-0000-0000-0000-000000000001"),
        SubCategoryId = Guid.Parse("c0000000-0000-0000-0000-000000000003"),
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000003"),
        RequestId = Guid.Parse("r0000000-0000-0000-0000-000000000003"),
        DepartmentId = Guid.Parse("d0000000-0000-0000-0000-000000000003"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Warehouse
    {
        Id = Guid.Parse("w0000000-0000-0000-0000-000000000004"),
        StockOutAmount = 1,
        StockInAmount = 4,
        GeneralStockAmount = 3,
        WaybillNumber = "WB-1004",
        WaybillPrice = "5400",
        ProductId = Guid.Parse("p0000000-0000-0000-0000-000000000004"),
        CategoryId = Guid.Parse("cat00000-0000-0000-0000-000000000003"),
        SubCategoryId = Guid.Parse("c0000000-0000-0000-0000-000000000004"),
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000004"),
        RequestId = Guid.Parse("r0000000-0000-0000-0000-000000000004"),
        DepartmentId = Guid.Parse("d0000000-0000-0000-0000-000000000004"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Warehouse
    {
        Id = Guid.Parse("w0000000-0000-0000-0000-000000000005"),
        StockOutAmount = 0,
        StockInAmount = 15,
        GeneralStockAmount = 15,
        WaybillNumber = "WB-1005",
        WaybillPrice = "11200",
        ProductId = Guid.Parse("p0000000-0000-0000-0000-000000000005"),
        CategoryId = Guid.Parse("cat00000-0000-0000-0000-000000000004"),
        SubCategoryId = Guid.Parse("c0000000-0000-0000-0000-000000000005"),
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000005"),
        RequestId = Guid.Parse("r0000000-0000-0000-0000-000000000005"),
        DepartmentId = Guid.Parse("d0000000-0000-0000-0000-000000000005"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Warehouse
    {
        Id = Guid.Parse("w0000000-0000-0000-0000-000000000006"),
        StockOutAmount = 5,
        StockInAmount = 10,
        GeneralStockAmount = 5,
        WaybillNumber = "WB-1006",
        WaybillPrice = "20000",
        ProductId = Guid.Parse("p0000000-0000-0000-0000-000000000006"),
        CategoryId = Guid.Parse("cat00000-0000-0000-0000-000000000004"),
        SubCategoryId = Guid.Parse("c0000000-0000-0000-0000-000000000006"),
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000006"),
        RequestId = Guid.Parse("r0000000-0000-0000-0000-000000000006"),
        DepartmentId = Guid.Parse("d0000000-0000-0000-0000-000000000006"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Warehouse
    {
        Id = Guid.Parse("w0000000-0000-0000-0000-000000000007"),
        StockOutAmount = 1,
        StockInAmount = 3,
        GeneralStockAmount = 2,
        WaybillNumber = "WB-1007",
        WaybillPrice = "3100",
        ProductId = Guid.Parse("p0000000-0000-0000-0000-000000000007"),
        CategoryId = Guid.Parse("cat00000-0000-0000-0000-000000000005"),
        SubCategoryId = Guid.Parse("c0000000-0000-0000-0000-000000000007"),
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000007"),
        RequestId = Guid.Parse("r0000000-0000-0000-0000-000000000007"),
        DepartmentId = Guid.Parse("d0000000-0000-0000-0000-000000000007"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Warehouse
    {
        Id = Guid.Parse("w0000000-0000-0000-0000-000000000008"),
        StockOutAmount = 0,
        StockInAmount = 4,
        GeneralStockAmount = 4,
        WaybillNumber = "WB-1008",
        WaybillPrice = "8900",
        ProductId = Guid.Parse("p0000000-0000-0000-0000-000000000008"),
        CategoryId = Guid.Parse("cat00000-0000-0000-0000-000000000001"),
        SubCategoryId = Guid.Parse("c0000000-0000-0000-0000-000000000001"),
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000008"),
        RequestId = Guid.Parse("r0000000-0000-0000-0000-000000000010"),
        DepartmentId = Guid.Parse("d0000000-0000-0000-0000-000000000001"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Warehouse
    {
        Id = Guid.Parse("w0000000-0000-0000-0000-000000000009"),
        StockOutAmount = 3,
        StockInAmount = 7,
        GeneralStockAmount = 4,
        WaybillNumber = "WB-1009",
        WaybillPrice = "12200",
        ProductId = Guid.Parse("p0000000-0000-0000-0000-000000000009"),
        CategoryId = Guid.Parse("cat00000-0000-0000-0000-000000000005"),
        SubCategoryId = Guid.Parse("c0000000-0000-0000-0000-000000000007"),
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000009"),
        RequestId = Guid.Parse("r0000000-0000-0000-0000-000000000009"),
        DepartmentId = Guid.Parse("d0000000-0000-0000-0000-000000000009"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    },
    new Warehouse
    {
        Id = Guid.Parse("w0000000-0000-0000-0000-000000000010"),
        StockOutAmount = 2,
        StockInAmount = 5,
        GeneralStockAmount = 3,
        WaybillNumber = "WB-1010",
        WaybillPrice = "6700",
        ProductId = Guid.Parse("p0000000-0000-0000-0000-000000000010"),
        CategoryId = Guid.Parse("cat00000-0000-0000-0000-000000000002"),
        SubCategoryId = Guid.Parse("c0000000-0000-0000-0000-000000000002"),
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000010"),
        RequestId = Guid.Parse("r0000000-0000-0000-0000-000000000008"),
        DepartmentId = Guid.Parse("d0000000-0000-0000-0000-000000000010"),
        CreatedDate = DateTime.Parse("2025-07-01"),
        UpdatedDate = null,
        DeletedDate = null,
        Status = Status.Active
    }
            );
        }
    }
}
