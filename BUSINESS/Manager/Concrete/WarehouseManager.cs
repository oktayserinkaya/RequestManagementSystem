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
    public class WarehouseManager(IWarehouseRepository service, IMapper mapper) : BaseManager<IWarehouseRepository, Warehouse>(service, mapper), IWarehouseManager
    {
    }
}
