using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.CategoryDTO
{
    public class CategorySelectListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
