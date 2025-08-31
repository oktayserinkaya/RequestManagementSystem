using System;
using CORE.Enums;

namespace WEB.Areas.RequestEvaluation.Models.RequestEvaluationVM
{
    public class RequestEvaluationListVM
    {
        public Guid Id { get; set; }
        public string CreatorFullName { get; set; } = "";
        public string CreatorEmail { get; set; } = "";
        public string DepartmentName { get; set; } = "";
        public string TitleName { get; set; } = "";

        public Status Status { get; set; }

        public string StatusText => Status switch
        {
            Status.Approved => "Onaylandı",
            Status.Rejected => "Reddedildi",
            _ => "Beklemede"
        };

        public DateTime CreatedDate { get; set; }
    }
}
