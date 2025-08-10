using System;
using Microsoft.AspNetCore.Http;
using DTO.Abstract;

namespace DTO.Concrete.RequestDTO
{
    public class CreateRequestDTO : BaseDTO
    {
        public DateTime? RequestDate { get; set; }
        public string? ProductFeaturesFilePath { get; set; }
        public IFormFile? ProductFeaturesFile { get; set; }
        public decimal? Amount { get; set; }
        public string? SpecialProductName { get; set; }

        public Guid? DepartmentId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SubCategoryId { get; set; }
        public Guid? ProductId { get; set; }

        public string? Description { get; set; }

        // ⬇️ Eksik olan alanlar bunlardı:
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? DepartmentName { get; set; }
    }
}
