using AutoMapper;
using BUSINESS.Manager.Concrete;
using BUSINESS.Manager.Interface;
using CORE.Enums;
using DTO.Concrete.RequestDTO;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Areas.Request.Controllers
{
    [Area("Request")]
    public class SubCategoriesController(ISubCategoryManager subCategoryManager, IMapper mapper) : Controller
    {
        private readonly ISubCategoryManager _subCategoryManager = subCategoryManager;
        private readonly IMapper _mapper = mapper;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetByCategoryId(string categoryId)
        {
            if (!Guid.TryParse(categoryId, out Guid categoryGuid))
            {
                return BadRequest("Geçersiz kategori ID!");
            }

            var subCategories = await _subCategoryManager.GetByDefaultsAsync<GetSubCategoryForSelectListDTO>(
                x => x.CategoryId == categoryGuid && x.Status != Status.Passive
            );

            return Json(subCategories ?? new List<GetSubCategoryForSelectListDTO>());
        }
    }


}
