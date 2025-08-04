using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.RequestDTO
{
    public class GetSubCategoryForSelectListDTO
    {
        public Guid Id { get; set; }
        public string? SubCategoryName { get; set; }
    }
}
