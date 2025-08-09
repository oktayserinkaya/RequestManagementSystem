
using System.Linq.Expressions;
using AutoMapper;
using BUSINESS.Manager.Interface;
using CORE.Entities.Concrete;
using CORE.Interface;

namespace BUSINESS.Manager.Concrete
{
    public class RequestManager : BaseManager<IRequestRepository, CORE.Entities.Concrete.Request>, IRequestManager
    {
        public RequestManager(IRequestRepository service, IMapper mapper)
            : base(service, mapper)
        {
        }

        public Task<int> GetCountAsync(Expression<Func<CORE.Entities.Concrete.Request, bool>>? predicate = null)
            => base.GetCountAsync(predicate);
    }
}
