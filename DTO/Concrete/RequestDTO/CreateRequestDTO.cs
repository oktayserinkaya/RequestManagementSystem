using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DTO.Abstract;
using Microsoft.AspNetCore.Http;

namespace DTO.Concrete.RequestDTO
{
    public class CreateRequestDTO : BaseDTO
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
