using CORE.Enums;
using WEB.Areas.Request.Models.Abstract;

namespace WEB.Areas.Request.Models.RequestVM
{
    using CORE.Enums;

    public class GetRequestsVM : BasePersonVM
    {
        public Guid Id { get; set; }
        public string DepartmentName { get; set; } = "";
        public string TitleName { get; set; } = "";
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? RequestDate { get; set; }
        public string Status { get; set; } = "";
        public Status StatusEnum { get; set; }


    }

}
