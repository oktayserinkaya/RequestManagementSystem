using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.PaymentDTO
{
    public class GetPaymentDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
