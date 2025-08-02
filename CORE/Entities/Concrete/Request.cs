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
    public class Request : BaseEntity
    {
        public Request()
        {
            Payments = [];
            Warehouses = [];
        }
        public DateTime? RequestDate { get; set; }
        public string? SpecialProductName { get; set; }
        public double Amount { get; set; }

        public string? ProductFeaturesFilePath { get; set; }
        public string? ProductFeatures { get; set; }

        [NotMapped]
        public IFormFile? ProductFeaturesFile { get; set; }

        public string? CommissionNote { get; set; }
        public bool IsApproved { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public Guid ProductId { get; set; }
        public Product? Product { get; set; }
        public Guid TitleId { get; set; }
        public Title? Title { get; set; }

        public List<Payment> Payments { get; set; }
        public List<Warehouse> Warehouses { get; set; }
    }
}
