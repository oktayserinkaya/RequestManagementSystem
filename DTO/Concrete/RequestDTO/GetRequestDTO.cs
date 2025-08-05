using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Concrete;
using CORE.Enums;
using DTO.Abstract;
using Microsoft.AspNetCore.Http;

namespace DTO.Concrete.RequestDTO
{
    public class GetRequestDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public DateTime? RequestDate { get; set; }       
        public Guid EmployeeId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public Guid ProductId { get; set; }
        public Status Status { get; set; }

    }
}
