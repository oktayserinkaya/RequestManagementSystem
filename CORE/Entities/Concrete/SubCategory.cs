using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Abstract;

namespace CORE.Entities.Concrete
{
    public class SubCategory : BaseEntity
    {
        public SubCategory()
        {
            Warehouses = [];
            Products = [];
        }
        public required string SubCategoryName { get; set; }
        public Guid CategoryId { get; set; }       

        public Category? Category { get; set; }

        public List<Warehouse> Warehouses { get; set; }

        public List<Product> Products { get; set; }

    }
}
