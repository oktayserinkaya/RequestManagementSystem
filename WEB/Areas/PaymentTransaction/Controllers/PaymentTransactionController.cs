using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using BUSINESS.Manager.Interface;
using CORE.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WEB.Areas.PaymentTransaction.Models.PaymentTransactionVM;

namespace WEB.Areas.PaymentTransaction.Controllers
{
    [Area("PaymentTransaction")]
    [Authorize(Roles = "OdemeBirimi,Admin")]
    [Route("PaymentTransaction/[controller]")]
    public class PaymentTransactionController : Controller
    {
        private readonly IRequestManager _requestManager;
        private readonly ILogger<PaymentTransactionController> _logger;

        public PaymentTransactionController(
            IRequestManager requestManager,
            ILogger<PaymentTransactionController> logger)
        {
            _requestManager = requestManager;
            _logger = logger;
        }

        [HttpGet("")] // => /PaymentTransaction/PaymentTransaction
        public async Task<IActionResult> Index(string? q = null, int take = 100)
        {
            var list = await _requestManager.GetFilteredListAsync(
                select: x => new PaymentTransactionListVM
                {
                    Id = x.Id,
                    RequestDate = x.RequestDate,
                    CreatedDate = x.CreatedDate,

                    EmployeeFullName = x.Employee != null
                        ? (x.Employee.FirstName + " " + x.Employee.LastName)
                        : string.Empty,
                    EmployeeEmail = x.Employee != null ? (x.Employee.Email ?? string.Empty) : string.Empty,
                    DepartmentName = (x.Employee != null && x.Employee.Department != null)
                        ? x.Employee.Department.DepartmentName
                        : string.Empty,

                    CategoryName = (x.Product != null && x.Product.SubCategory != null && x.Product.SubCategory.Category != null)
                        ? (x.Product.SubCategory.Category.CategoryName ?? string.Empty)
                        : string.Empty,
                    SubCategoryName = (x.Product != null && x.Product.SubCategory != null)
                        ? (x.Product.SubCategory.SubCategoryName ?? string.Empty)
                        : string.Empty,
                    ProductName = (x.Product != null && !string.IsNullOrWhiteSpace(x.Product.ProductName))
                        ? x.Product.ProductName!
                        : (x.SpecialProductName ?? "-"),

                    Amount = x.Amount,
                    SpecPath = x.ProductFeaturesFilePath
                },
                where: x =>
                    x.Status == Status.Modified &&
                    (string.IsNullOrWhiteSpace(q) ||
                     ((x.Employee!.FirstName + " " + x.Employee.LastName).ToLower().Contains(q!.ToLower())) ||
                     ((x.Employee.Email ?? string.Empty).ToLower().Contains(q!.ToLower())) ||
                     ((x.Employee.Department!.DepartmentName ?? string.Empty).ToLower().Contains(q!.ToLower()))
                    ),
                orderBy: qy => qy.OrderByDescending(z => z.CreatedDate),
                join: qy => qy
                    .Include(r => r.Employee!).ThenInclude(e => e.Department!)
                    .Include(r => r.Product!).ThenInclude(p => p.SubCategory!).ThenInclude(sc => sc.Category!)
            );

            var model = list.Take(take).ToList();
            return View(model);
        }
    }
}
