using AutoMapper;
using BUSINESS.Manager.Interface;
using CORE.Enums;
using DTO.Concrete.RequestDTO;
using Microsoft.AspNetCore.Mvc;

namespace WEB.Areas.Request.Controllers
{
    [Area("Request")]
    public class ProductsController(IProductManager productManager, IMapper mapper) : Controller
    {
        private readonly IProductManager _productManager = productManager;
        private readonly IMapper _mapper = mapper;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetBySubCategoryId(string subCategoryId)
        {
            // 1. Gelen ID'yi kontrol et
            if (!Guid.TryParse(subCategoryId, out Guid subCategoryGuid))
            {
                return BadRequest("Geçersiz alt kategori ID!");
            }

            // 2. Alt kategoriye ait aktif ürünleri getir
            var products = await _productManager.GetByDefaultsAsync<GetProductForSelectListDTO>(
                x => x.SubCategoryId == subCategoryGuid && x.Status != Status.Passive
            );

            // 3. Hiç ürün yoksa boş liste döndür
            return Json(products ?? new List<GetProductForSelectListDTO>());
        }
    }
}
