using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB.Areas.Request.Models.Abstract;

namespace WEB.Areas.Request.Models.RequestVM
{
    public class CreateRequestVM : BasePersonVM
    {
        public DateTime? RequestDate { get; set; }

        public string? ProductFeaturesFilePath { get; set; }

        public IFormFile? ProductFeaturesFile { get; set; }

        [Required(ErrorMessage = "Miktar alanı zorunludur.")]
        [Range(1, double.MaxValue, ErrorMessage = "Miktar en az 1 olmalıdır.")]
        public double Amount { get; set; }

        public string? SpecialProductName { get; set; }

        public string? DepartmentName { get; set; } // Sadece görüntü için (readonly input)

        [Required(ErrorMessage = "Kategori seçimi zorunludur.")]
        public Guid? CategoryId { get; set; }

        [Required(ErrorMessage = "Alt kategori seçimi zorunludur.")]
        public Guid? SubCategoryId { get; set; }

        [Required(ErrorMessage = "Ürün seçimi zorunludur.")]
        public Guid? ProductId { get; set; }

        public string? Description { get; set; }

        // Bu alanlar View'da yoksa validation'a sokulmamalı
        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }
        public string? ProductName { get; set; }
        public Guid? DepartmentId { get; set; }
    }
}
