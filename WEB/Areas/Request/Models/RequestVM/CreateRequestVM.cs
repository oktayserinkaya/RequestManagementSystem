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
        public double Amount { get; set; }
        public string? SpecialProductName { get; set; }
        public required string DepartmentName { get; set; }
        public required string CategoryName { get; set; }
        public required string SubCategoryName { get; set; }
        public required string ProductName { get; set; }
        [Required]
        public Guid? DepartmentId { get; set; }
        [Required]
        public Guid? CategoryId { get; set; }
        [Required]
        public Guid? SubCategoryId { get; set; }
        [Required]
        public Guid? ProductId { get; set; }
        public string? Description { get; set; }

    }
}
