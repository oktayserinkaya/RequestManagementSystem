using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Abstract;

namespace CORE.Entities.Concrete
{
    public class Title : BaseEntity
    {
        public Title()
        {
            Employees = [];
        }
        public required string TitleName { get; set; }

        public List<Employee> Employees { get; set; }


    }
}
