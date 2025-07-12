using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Abstract;

namespace CORE.Entities.Concrete
{
    public class SubSubCategory : BaseEntity
    {
        public SubSubCategory()
        {
            SubCategories = [];
            Products = [];
            Warehouses = [];
        }
        public required string SubSubCategoryName { get; set; }

        public Guid SubCategoryId { get; set; }

        public SubCategory? SubCategory { get; set; }

        public List<Product> Products { get; set; }

        public List<SubCategory> SubCategories { get; set; }
        public List<Warehouse> Warehouses { get; set; }

    }
}
