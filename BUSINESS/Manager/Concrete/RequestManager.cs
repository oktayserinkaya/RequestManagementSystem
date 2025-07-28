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
    public class RequestManager(IRequestRepository service, IMapper mapper) : BaseManager<IRequestRepository, Request>(service, mapper), IRequestManager
    {
    }
}
