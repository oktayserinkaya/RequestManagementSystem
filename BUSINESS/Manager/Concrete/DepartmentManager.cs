using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BUSINESS.Manager.Interface;
using CORE.Entities.Concrete;
using CORE.Interface;

namespace BUSINESS.Manager.Concrete
{
    public class DepartmentManager(IDepartmentRepository service, IMapper mapper) : BaseManager<IDepartmentRepository, Department>(service, mapper), IDepartmentManager
    {
    }
}
