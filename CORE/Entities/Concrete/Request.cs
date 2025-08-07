using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Abstract;
using CORE.IdentityEntities;
using Microsoft.AspNetCore.Http;

namespace CORE.Entities.Concrete
{
    public class Request : BaseEntity
    {
        public Request()
        {
            Payments = new List<Payment>();
            Warehouses = new List<Warehouse>();
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

        // AppUser ilişkisi (sadece ID tutulacak)
        public Guid AppUserId { get; set; }

        // Diğer ilişkiler
        public Guid EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; }

        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }

        public Guid TitleId { get; set; }
        public virtual Title? Title { get; set; }

        public Guid DepartmentId { get; set; }

        public virtual List<Payment> Payments { get; set; }
        public virtual List<Warehouse> Warehouses { get; set; }
    }
}