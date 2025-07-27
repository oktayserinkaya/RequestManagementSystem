using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Concrete;
using CORE.Interface;
using DATAACCESS.Context;

namespace DATAACCESS.Services
{
    public class SubCategoryService(AppDbContext context) : BaseService<SubCategory>(context), ISubCategoryRepository
    {
    }
}
