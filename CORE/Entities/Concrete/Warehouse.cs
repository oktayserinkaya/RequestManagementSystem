using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Abstract;

namespace CORE.Entities.Concrete
{
    public class Warehouse : BaseEntity
    {
        public double StockOutAmount { get; set; }
        public double StockInAmount { get; set; }
        public double GeneralStockAmount { get; set; }
        public required string WaybillNumber { get; set; }
        public required string WaybillPrice { get; set; }

        public Guid ProductId { get; set; }
        public Product? Product { get; set; }

        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        public Guid SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }

        public Guid EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public Guid RequestId { get; set; }
        public Request? Request { get; set; }

        public Guid DepartmentId { get; set; }
        public Department? Department { get; set; }
    }
}
