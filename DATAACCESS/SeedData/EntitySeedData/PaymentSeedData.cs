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
    public class PaymentSeedData : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasData(
                    new Payment
                    {
                        Id = Guid.Parse("a1b1c1d1-e1f1-1111-aaaa-000000000001"),
                        AmountToPay = 1250.50,
                        PaymentDate = new DateTime(2025, 1, 10),
                        IsPaid = true,
                        InvoiceNumber = "INV-1001",
                        RequestId = Guid.Parse("r0000000-0000-0000-0000-000000000001"),
                        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000001"),
                        CreatedDate = DateTime.Now,
                        Status = Status.Active
                    },
    new Payment
    {
        Id = Guid.Parse("a1b1c1d1-e1f1-1111-aaaa-000000000002"),
        AmountToPay = 980.00,
        PaymentDate = new DateTime(2025, 2, 5),
        IsPaid = false,
        InvoiceNumber = "INV-1002",
        RequestId = Guid.Parse("r0000000-0000-0000-0000-000000000002"),
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000002"),
        CreatedDate = DateTime.Now,
        Status = Status.Passive
    },
    new Payment
    {
        Id = Guid.Parse("a1b1c1d1-e1f1-1111-aaaa-000000000003"),
        AmountToPay = 450.75,
        PaymentDate = new DateTime(2025, 3, 12),
        IsPaid = true,
        InvoiceNumber = "INV-1003",
        RequestId = Guid.Parse("r0000000-0000-0000-0000-000000000003"),
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000003"),
        CreatedDate = DateTime.Now,
        Status = Status.Active
    },
    new Payment
    {
        Id = Guid.Parse("a1b1c1d1-e1f1-1111-aaaa-000000000004"),
        AmountToPay = 1675.20,
        PaymentDate = new DateTime(2025, 4, 3),
        IsPaid = true,
        InvoiceNumber = "INV-1004",
        RequestId = Guid.Parse("r0000000-0000-0000-0000-000000000004"),
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000004"),
        CreatedDate = DateTime.Now,
        Status = Status.Modified
    },
    new Payment
    {
        Id = Guid.Parse("a1b1c1d1-e1f1-1111-aaaa-000000000005"),
        AmountToPay = 800.00,
        PaymentDate = new DateTime(2025, 5, 15),
        IsPaid = false,
        InvoiceNumber = "INV-1005",
        RequestId = Guid.Parse("r0000000-0000-0000-0000-000000000005"),
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000005"),
        CreatedDate = DateTime.Now,
        Status = Status.Active
    },
    new Payment
    {
        Id = Guid.Parse("a1b1c1d1-e1f1-1111-aaaa-000000000006"),
        AmountToPay = 3000.00,
        PaymentDate = new DateTime(2025, 6, 7),
        IsPaid = true,
        InvoiceNumber = "INV-1006",
        RequestId = Guid.Parse("r0000000-0000-0000-0000-000000000006"),
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000006"),
        CreatedDate = DateTime.Now,
        Status = Status.Active
    },
    new Payment
    {
        Id = Guid.Parse("a1b1c1d1-e1f1-1111-aaaa-000000000007"),
        AmountToPay = 1500.00,
        PaymentDate = new DateTime(2025, 6, 20),
        IsPaid = true,
        InvoiceNumber = "INV-1007",
        RequestId = Guid.Parse("r0000000-0000-0000-0000-000000000007"),
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000007"),
        CreatedDate = DateTime.Now,
        Status = Status.Passive
    },
    new Payment
    {
        Id = Guid.Parse("a1b1c1d1-e1f1-1111-aaaa-000000000008"),
        AmountToPay = 2100.40,
        PaymentDate = new DateTime(2025, 7, 1),
        IsPaid = false,
        InvoiceNumber = "INV-1008",
        RequestId = Guid.Parse("r0000000-0000-0000-0000-000000000008"),
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000008"),
        CreatedDate = DateTime.Now,
        Status = Status.Active
    },
    new Payment
    {
        Id = Guid.Parse("a1b1c1d1-e1f1-1111-aaaa-000000000009"),
        AmountToPay = 600.00,
        PaymentDate = new DateTime(2025, 7, 10),
        IsPaid = true,
        InvoiceNumber = "INV-1009",
        RequestId = Guid.Parse("r0000000-0000-0000-0000-000000000009"),
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000009"),
        CreatedDate = DateTime.Now,
        Status = Status.Modified
    },
    new Payment
    {
        Id = Guid.Parse("a1b1c1d1-e1f1-1111-aaaa-000000000010"),
        AmountToPay = 720.00,
        PaymentDate = new DateTime(2025, 7, 12),
        IsPaid = false,
        InvoiceNumber = "INV-1010",
        RequestId = Guid.Parse("r0000000-0000-0000-0000-000000000010"),
        EmployeeId = Guid.Parse("e0000000-0000-0000-0000-000000000010"),
        CreatedDate = DateTime.Now,
        Status = Status.Active
    }
            );
        }
    }
}
