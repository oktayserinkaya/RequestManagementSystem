namespace WEB.Areas.RequestEvaluation.Models.RequestEvaluationVM
{
    public class RequestEvaluationListVM
    {
        public Guid Id { get; set; }
        public string CreatorFullName { get; set; } = "";
        public string CreatorEmail { get; set; } = "";
        public string DepartmentName { get; set; } = "";
        public string TitleName { get; set; } = "";
        public string StatusText { get; set; } = "";
        public DateTime CreatedDate { get; set; }
    }
}
    