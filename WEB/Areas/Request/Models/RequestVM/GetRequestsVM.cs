using CORE.Enums;
using WEB.Areas.Request.Models.Abstract;

namespace WEB.Areas.Request.Models.RequestVM
{
    using CORE.Enums;

    public class GetRequestsVM : BasePersonVM
    {
        public Guid Id { get; set; }
        public DateTime? RequestDate { get; set; }
        public required string TitleName { get; set; }
        public required string DepartmentName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public Status StatusEnum { get; set; }
        public string Status { get; set; } = string.Empty;

    }

}
