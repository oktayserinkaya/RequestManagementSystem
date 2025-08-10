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

        // Null-safe FullName
        public string FullName
        {
            get
            {
                var first = FirstName ?? string.Empty;
                var last = LastName ?? string.Empty;
                return $"{first} {last}".Trim();
            }
        }
    }
}
