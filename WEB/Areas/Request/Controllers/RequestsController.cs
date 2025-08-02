using System.Threading.Tasks;
using AutoMapper;
using BUSINESS.Manager.Interface;
using CORE.Enums;
using CORE.Extensions;
using DTO.Concrete.RequestDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB.Areas.Request.Models.RequestVM;

namespace WEB.Areas.Request.Controllers
{
    [Area("Request")]
    public class RequestsController(IRequestManager requestManager, IMapper mapper) : Controller
    {
        private readonly IRequestManager _requestManager = requestManager;
        private readonly IMapper _mapper = mapper;

        public async Task<IActionResult> Index()
        {
            var model = await _requestManager.GetFilteredListAsync(
                select: x => new GetRequestsVM
                {
                    Id = x.Id,
                    RequestDate = x.RequestDate,

                    // FullName için kaynaklar:
                    FirstName = x.Employee != null ? x.Employee.FirstName : null,
                    LastName = x.Employee != null ? x.Employee.LastName : null,
                    Email = x.Employee != null ? x.Employee.Email : null,

                    // İlişkiler:
                    DepartmentName = (x.Employee != null && x.Employee.Department != null)
                                        ? x.Employee.Department.DepartmentName
                                        : string.Empty,

                    // Title Request’teyse:
                    TitleName = (x.Title != null) ? x.Title.TitleName : string.Empty,

                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,

                    // STRING yerine ENUM'u taşı
                    StatusEnum = x.Status
                },
                where: x => x.Status != Status.Passive,
                orderBy: q => q.OrderByDescending(z => z.CreatedDate),
                join: q => q
                    .Include(z => z.Employee!).ThenInclude(z => z.Department!)
                    .Include(z => z.Title!)
            );

            // EF sorgusu bitti → bellek tarafında DisplayName'e çevir
            foreach (var item in model)
                item.Status = item.StatusEnum.GetDisplayName();

            return View(model);
        }

        public IActionResult CreateRequest() => View();

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRequest(CreateRequestVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Aşağıdaki kurallara uyunuz!!";
            }

            var dto = _mapper.Map<CreateRequestDTO>(model);
            var result = await _requestManager.AddAsync(dto);

            if (!result)
            {
                TempData["Error"] = "Talep oluşturulamadı!!";
                return View(model);
            }

            TempData["Success"] = "Talep başarılı bir şekilde oluşturuldu";
            return RedirectToAction("Index");
        }
    }
}
