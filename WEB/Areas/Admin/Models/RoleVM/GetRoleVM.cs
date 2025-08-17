using System.ComponentModel.DataAnnotations;

namespace WEB.Areas.Admin.Models.RoleVM
{
    public class GetRoleVM
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedDate { get; set; }   // non-nullable
        public DateTime? UpdatedDate { get; set; }  // nullable
        public string Status { get; set; } = string.Empty;
    }
}
