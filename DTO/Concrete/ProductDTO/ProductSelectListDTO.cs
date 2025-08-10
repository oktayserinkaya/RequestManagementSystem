using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.ProductDTO
{
    public class ProductSelectListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public Guid SubCategoryId { get; set; }
    }
}
