using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Abstract;

namespace DTO.Concrete.RequestDTO
{
    public class GetCategoryForSelectListDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; } = null!;
    }

}
