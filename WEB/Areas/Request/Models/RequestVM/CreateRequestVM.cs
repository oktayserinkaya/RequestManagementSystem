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
        public Guid? DepartmentId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public Guid? ProductId { get; set; }
        public string? Description { get; set; }

    }
}
