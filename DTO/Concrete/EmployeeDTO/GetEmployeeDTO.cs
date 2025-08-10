using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.EmployeeDTO
{
    public class GetEmployeeDTO
    {
        public Guid Id { get; set; }
        public Guid AppUserId { get; set; }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

        public Guid? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public Guid? TitleId { get; set; }
        public string? TitleName { get; set; }
    }
}
