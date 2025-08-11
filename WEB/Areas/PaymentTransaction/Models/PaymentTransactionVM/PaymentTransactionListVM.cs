namespace WEB.Areas.PaymentTransaction.Models.PaymentTransactionVM
{
    public class PaymentTransactionListVM
    {
        public Guid Id { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public string EmployeeFullName { get; set; } = "";
        public string EmployeeEmail { get; set; } = "";
        public string DepartmentName { get; set; } = "";

        public string CategoryName { get; set; } = "";
        public string SubCategoryName { get; set; } = "";
        public string ProductName { get; set; } = "";

        public decimal? Amount { get; set; }
        public string? SpecPath { get; set; }
    }
}
