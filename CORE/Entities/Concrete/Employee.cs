using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Abstract;
using Microsoft.AspNetCore.Http;

namespace CORE.Entities.Concrete
{
    public class Employee : BasePerson
    {
        public Employee()
        {
            Requests = [];
            Payments = [];
            Warehouses = [];
        }
        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }

        public Guid TitleId { get; set; }
        public Title? Title { get; set; }
        public Guid DepartmentId { get; set; }
        public Department? Department { get; set; }
        public List<Request> Requests { get; set; }
        public List<Payment> Payments { get; set; }
        public List<Warehouse> Warehouses { get; set; }


    }
}
