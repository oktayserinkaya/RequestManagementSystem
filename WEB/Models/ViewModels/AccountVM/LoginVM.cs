using System.ComponentModel.DataAnnotations;

namespace WEB.Models.ViewModels.AccountVM
{
    public class LoginVM
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
