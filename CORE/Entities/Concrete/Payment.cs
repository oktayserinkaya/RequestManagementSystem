using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Abstract;

namespace CORE.Entities.Concrete
{
    public class Payment : BaseEntity
    {
        public double AmountToPay { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsPaid { get; set; }
        public required string InvoiceNumber { get; set; }

        public Guid RequestId { get; set; }
        public Request? Request  { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
