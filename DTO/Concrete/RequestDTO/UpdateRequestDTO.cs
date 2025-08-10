// DTO/Concrete/RequestDTO/UpdateRequestDTO.cs
using System;
using System.ComponentModel.DataAnnotations;
using DTO.Abstract;
using Microsoft.AspNetCore.Http;

namespace DTO.Concrete.RequestDTO
{
    public class UpdateRequestDTO : BaseDTO
    {
        [Required] 
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Talep tarihi zorunludur.")]
        public DateTime? RequestDate { get; set; }

        public string? SpecialProductName { get; set; }
        public decimal? Amount { get; set; }
        public string? Description { get; set; }

        // Güncellemede dokunmak istemediklerinizi null bırakabilirsiniz
        public Guid? AppUserId { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? DepartmentId { get; set; }
        public Guid? TitleId { get; set; }

        [Required(ErrorMessage = "Ürün seçilmelidir.")]
        public Guid? ProductId { get; set; }

        public IFormFile? ProductFeaturesFile { get; set; }
        public string? ProductFeaturesFilePath { get; set; }
    }
}
