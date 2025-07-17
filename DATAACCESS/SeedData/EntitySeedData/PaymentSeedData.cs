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
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    AmountToPay = 2200.00,
                    PaymentDate = new DateTime(2025, 8, 5),
                    IsPaid = true,
                    InvoiceNumber = "INV-2001",
                    RequestId = Guid.Parse("10000000-0000-0000-0000-0000000000ab"), // Request tablosunda olmalı
                    EmployeeId = Guid.Parse("11111111-1111-1111-1111-111111111111"), // Employee tablosunda olmalı
                    CreatedDate = DateTime.Parse("2025-08-01"),
                    Status = Status.Active
                },
                new Payment
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    AmountToPay = 1450.50,
                    PaymentDate = new DateTime(2025, 8, 10),
                    IsPaid = false,
                    InvoiceNumber = "INV-2002",
                    RequestId = Guid.Parse("10000000-0000-0000-0000-0000000000ac"),
                    EmployeeId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    CreatedDate = DateTime.Parse("2025-08-01"),
                    Status = Status.Passive
                },
                new Payment
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    AmountToPay = 3100.75,
                    PaymentDate = new DateTime(2025, 8, 12),
                    IsPaid = true,
                    InvoiceNumber = "INV-2003",
                    RequestId = Guid.Parse("10000000-0000-0000-0000-0000000000ad"),
                    EmployeeId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    CreatedDate = DateTime.Parse("2025-08-01"),
                    Status = Status.Modified
                },
                new Payment
                {
                    Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    AmountToPay = 860.90,
                    PaymentDate = new DateTime(2025, 8, 15),
                    IsPaid = false,
                    InvoiceNumber = "INV-2004",
                    RequestId = Guid.Parse("10000000-0000-0000-0000-0000000000ae"),
                    EmployeeId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    CreatedDate = DateTime.Parse("2025-08-01"),
                    Status = Status.Active
                }
            );
        }
    }



}
