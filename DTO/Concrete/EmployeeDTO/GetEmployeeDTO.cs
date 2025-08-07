﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.EmployeeDTO
{
    public class GetEmployeeDTO
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public Guid DepartmentId { get; set; }

        public string? DepartmentName { get; set; }
    }
}
