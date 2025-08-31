
using DTO.Abstract;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using CORE.Enums;

namespace DTO.Concrete.RequestDTO
{
    public class UpdateRequestDTO : BaseDTO
    {
        [Required] public Guid Id { get; set; }    
        public DateTime? RequestDate { get; set; }

        public string? SpecialProductName { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }   

        public Guid? AppUserId { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? TitleId { get; set; }

        public Guid? ProductId { get; set; }

        public IFormFile? ProductFeaturesFile { get; set; }
        public string? ProductFeaturesFilePath { get; set; }

        public CORE.Enums.Status Status { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? CommissionNote { get; set; }
    }
}
