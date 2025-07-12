using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Abstract;

namespace CORE.Entities.Concrete
{
    public class Department : BaseEntity
    {
        public Department()
        {
            Employees = [];
            Warehouses = [];
        }
        public required string DepartmentName { get; set; }

        public List<Employee> Employees { get; set; }
        public List<Warehouse> Warehouses { get; set; }
    }
}
