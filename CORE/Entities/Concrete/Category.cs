using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Abstract;

namespace CORE.Entities.Concrete
{
    public class Category : BaseEntity
    {
        public Category()
        {
            SubCategories = [];
            Warehouses = [];
        }
        public required string CategoryName { get; set; }

        public List<SubCategory> SubCategories { get; set; }
        public List<Warehouse> Warehouses { get; set; }


    }
}
