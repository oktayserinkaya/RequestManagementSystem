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
    public class Product : BaseEntity
    {
        public Product()
        {
            Requests = [];
            Warehouses = [];
        }
        public required string ProductName { get; set; }
        public string? ImagePath { get; set; }

        [NotMapped]
        public IFormFile? Image { get; set; }

        public double StockAmount { get; set; }
        public int MyProperty { get; set; }

        public Guid SubSubCategoryId { get; set; }
        public SubSubCategory? SubSubCategory { get; set; }

        public List<Request> Requests { get; set; }
        public List<Warehouse> Warehouses { get; set; }
    }
}
