using System.Linq;
using System.Threading.Tasks;
using BUSINESS.Manager.Interface;
using CORE.Enums;
using CORE.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.Areas.Admin.Models.DashboardVM;     // DashboardIndexVM
using WEB.Areas.Request.Models.RequestVM;      // GetRequestsVM
using RequestEntity = CORE.Entities.Concrete.Request;

namespace WEB.Controllers
{
    [Authorize]
    [Route("")]
    public class HomeController : Controller
    {
        private readonly IRequestManager _requestManager;
        public HomeController(IRequestManager requestManager) => _requestManager = requestManager;

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var entities = await _requestManager.GetByDefaultsAsync<RequestEntity>(
                x => x.Status != Status.Passive,
                join: q => q
                    .Include(z => z.Employee)!.ThenInclude(z => z!.Department!)
                    .Include(z => z.Title!)
            );

            var total = entities.Count;
            var pending = entities.Count(x => x.Status == Status.Active);
            var completed = entities.Count(x => x.Status == Status.Modified);

            var list = entities
                .Select(x => new GetRequestsVM
                {
                    Id = x.Id,
                    FirstName = x.Employee?.FirstName,
                    LastName = x.Employee?.LastName,
                    Email = x.Employee?.Email,
                    DepartmentName = x.Employee?.Department?.DepartmentName ?? string.Empty,
                    TitleName = x.Title?.TitleName ?? string.Empty,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    RequestDate = x.RequestDate,
                    Status = x.Status.GetDisplayName()
                })
                .OrderByDescending(m => m.CreatedDate)
                .ToList();

            var vm = new DashboardIndexVM
            {
                TotalRequests = total,
                PendingRequests = pending,
                CompletedRequests = completed,
                Requests = list
            };

            return View(vm);
        }
    }
}
