using System;
using CORE.Entities.Concrete;
using CORE.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PaymentSeedData : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasData(
            new Payment { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), AmountToPay = 42000.00, PaymentDate = new DateTime(2025, 8, 5), IsPaid = true, InvoiceNumber = "INV-3001", RequestId = Guid.Parse("10000000-0000-0000-0000-000000000001"), EmployeeId = Guid.Parse("e1111111-1111-1111-1111-111111111111"), CreatedDate = DateTime.Parse("2025-08-01"), Status = Status.Active },
            new Payment { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), AmountToPay = 9500.50, PaymentDate = new DateTime(2025, 8, 6), IsPaid = false, InvoiceNumber = "INV-3002", RequestId = Guid.Parse("10000000-0000-0000-0000-000000000002"), EmployeeId = Guid.Parse("e2222222-2222-2222-2222-222222222222"), CreatedDate = DateTime.Parse("2025-08-01"), Status = Status.Active },
            new Payment { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), AmountToPay = 7200.75, PaymentDate = new DateTime(2025, 8, 7), IsPaid = true, InvoiceNumber = "INV-3003", RequestId = Guid.Parse("10000000-0000-0000-0000-000000000003"), EmployeeId = Guid.Parse("e3333333-3333-3333-3333-333333333333"), CreatedDate = DateTime.Parse("2025-08-01"), Status = Status.Modified },
            new Payment { Id = Guid.Parse("44444444-4444-4444-4444-444444444444"), AmountToPay = 16850.90, PaymentDate = new DateTime(2025, 8, 8), IsPaid = false, InvoiceNumber = "INV-3004", RequestId = Guid.Parse("10000000-0000-0000-0000-000000000004"), EmployeeId = Guid.Parse("e4444444-4444-4444-4444-444444444444"), CreatedDate = DateTime.Parse("2025-08-01"), Status = Status.Active },
            new Payment { Id = Guid.Parse("55555555-5555-5555-5555-555555555555"), AmountToPay = 11000.00, PaymentDate = new DateTime(2025, 8, 9), IsPaid = true, InvoiceNumber = "INV-3005", RequestId = Guid.Parse("10000000-0000-0000-0000-000000000005"), EmployeeId = Guid.Parse("e5555555-5555-5555-5555-555555555555"), CreatedDate = DateTime.Parse("2025-08-01"), Status = Status.Active },
            new Payment { Id = Guid.Parse("66666666-6666-6666-6666-666666666666"), AmountToPay = 54000.00, PaymentDate = new DateTime(2025, 8, 10), IsPaid = true, InvoiceNumber = "INV-3006", RequestId = Guid.Parse("10000000-0000-0000-0000-000000000006"), EmployeeId = Guid.Parse("e6666666-6666-6666-6666-666666666666"), CreatedDate = DateTime.Parse("2025-08-01"), Status = Status.Active },
            new Payment { Id = Guid.Parse("77777777-7777-7777-7777-777777777777"), AmountToPay = 7800.00, PaymentDate = new DateTime(2025, 8, 11), IsPaid = false, InvoiceNumber = "INV-3007", RequestId = Guid.Parse("10000000-0000-0000-0000-000000000007"), EmployeeId = Guid.Parse("e7777777-7777-7777-7777-777777777777"), CreatedDate = DateTime.Parse("2025-08-01"), Status = Status.Active },
            new Payment { Id = Guid.Parse("88888888-8888-8888-8888-888888888888"), AmountToPay = 9300.00, PaymentDate = new DateTime(2025, 8, 12), IsPaid = true, InvoiceNumber = "INV-3008", RequestId = Guid.Parse("10000000-0000-0000-0000-000000000008"), EmployeeId = Guid.Parse("e8888888-8888-8888-8888-888888888888"), CreatedDate = DateTime.Parse("2025-08-01"), Status = Status.Active },
            new Payment { Id = Guid.Parse("99999999-9999-9999-9999-999999999999"), AmountToPay = 25600.00, PaymentDate = new DateTime(2025, 8, 13), IsPaid = true, InvoiceNumber = "INV-3009", RequestId = Guid.Parse("10000000-0000-0000-0000-000000000009"), EmployeeId = Guid.Parse("e9999999-9999-9999-9999-999999999999"), CreatedDate = DateTime.Parse("2025-08-01"), Status = Status.Active },
            new Payment { Id = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), AmountToPay = 12400.00, PaymentDate = new DateTime(2025, 8, 14), IsPaid = false, InvoiceNumber = "INV-3010", RequestId = Guid.Parse("10000000-0000-0000-0000-000000000010"), EmployeeId = Guid.Parse("eaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), CreatedDate = DateTime.Parse("2025-08-01"), Status = Status.Active }
        );
    }
}
