using System.Linq;
using System.Threading.Tasks;
using BUSINESS.Manager.Interface;
using CORE.Enums;
using CORE.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.Areas.Request.Models.RequestVM;
using WEB.Areas.Admin.Models.DashboardVM;
using RequestEntity = CORE.Entities.Concrete.Request;
using Microsoft.AspNetCore.Authorization;

namespace WEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class DashboardController : Controller
    {
        private readonly IRequestManager _requestManager;
        public DashboardController(IRequestManager requestManager) => _requestManager = requestManager;

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var entities = await _requestManager.GetByDefaultsAsync<RequestEntity>(
                x => x.Status != Status.Passive,
                join: q => q
                    .Include(z => z.Employee)!.ThenInclude(z => z!.Department!)
                    .Include(z => z.Title!)
            );

            // Kart sayıları (ihtiyacınıza göre uyarlayın)
            var total = entities.Count;
            var pending = entities.Count(x => x.Status == Status.Active);    // örnek: Pending ~ Active
            var completed = entities.Count(x => x.Status == Status.Modified); // örnek: Completed ~ Modified

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
