namespace WEB.Areas.PurchaseTransaction.Models.PurchaseTransactionVM
{
    public class OrderListVM
    {
        public Guid Id { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public string EmployeeFullName { get; set; } = string.Empty;
        public string EmployeeEmail { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;

        public string CategoryName { get; set; } = "-";
        public string SubCategoryName { get; set; } = "-";
        public string ProductName { get; set; } = "-";

        public decimal? Amount { get; set; }
        public string? SpecPath { get; set; }
        public string StatusText { get; set; } = "-";
    }
}
