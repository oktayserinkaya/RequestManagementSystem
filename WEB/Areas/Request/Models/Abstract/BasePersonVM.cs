using System.ComponentModel.DataAnnotations;

namespace WEB.Areas.Request.Models.Abstract
{
    public class BasePersonVM
    {
        [Display(Name = "Ad")]
        public string? FirstName { get; set; }

        [Display(Name = "Soyad")]
        public string? LastName { get; set; }

        [Display(Name = "E-Mail")]
        public string? Email { get; set; }

        [Display(Name = "Doğum Tarihi")]
        public DateOnly? Birthdate { get; set; }

        // Bu property readonly; sadece FirstName + LastName'ı birleştirir
        public string? FullName => $"{FirstName} {LastName}";
    }


}
