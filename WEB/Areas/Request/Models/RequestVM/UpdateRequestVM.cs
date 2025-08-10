// WEB/Areas/Request/Models/RequestVM/UpdateRequestVM.cs
using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WEB.Areas.Request.Models.RequestVM
{
    public class UpdateRequestVM
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Talep tarihi zorunludur.")]
        public DateTime? RequestDate { get; set; }

        public string? SpecialProductName { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }

        // Görüntüleme alanları
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName => string.Join(" ", new[] { FirstName, LastName }.Where(x => !string.IsNullOrWhiteSpace(x)));
        public Guid? DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        // Seçimler
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }

        [Required(ErrorMessage = "Ürün seçilmelidir.")]
        public Guid? ProductId { get; set; }

        // Dosya alanları
        public IFormFile? ProductFeaturesFile { get; set; }
        public string? ExistingProductFeaturesFilePath { get; set; } // ekranda readonly göstermek için
        public string? ProductFeaturesFilePath { get; set; }          // uyumluluk (projedeki olası kullanımlar için)

        // Uyum için ekliyoruz (bazı yerlerde referans olabilir)
        public Guid? AppUserId { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? TitleId { get; set; }

        // Dropdown listeler
        public SelectList? CategoryList { get; set; }
        public SelectList? SubCategoryList { get; set; }
        public SelectList? ProductList { get; set; }



    }
}
