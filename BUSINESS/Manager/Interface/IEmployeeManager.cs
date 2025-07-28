using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Concrete;
using CORE.Interface;

namespace BUSINESS.Manager.Interface
{
    public interface IEmployeeManager : IBaseManager<IEmployeeRepository, Employee>
    {
    }
}
