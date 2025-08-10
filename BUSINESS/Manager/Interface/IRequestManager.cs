using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Concrete;
using CORE.Interface;

namespace BUSINESS.Manager.Interface
{
    public interface IRequestManager : IBaseManager<IRequestRepository, Request>
    {
        Task<int> GetCountAsync(Expression<Func<Request, bool>>? predicate = null);
    }
}
