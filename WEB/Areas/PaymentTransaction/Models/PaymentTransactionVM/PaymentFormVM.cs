using System.ComponentModel.DataAnnotations;

namespace WEB.Areas.PaymentTransaction.Models.PaymentTransactionVM
{
    public class PaymentFormVM
    {
        // Talep özeti (readonly göstereceğiz)
        public Guid RequestId { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? EmployeeFullName { get; set; }
        public string? DepartmentName { get; set; }
        public string? ProductName { get; set; }
        public decimal? RequestedAmount { get; set; }
        public string? SpecPath { get; set; }

        // Tedarikçi
        [Required] public string SupplierName { get; set; } = "";
        public string? SupplierTaxNo { get; set; }
        public string? SupplierIban { get; set; }
        [EmailAddress] public string? SupplierEmail { get; set; }
        public string? SupplierPhone { get; set; }

        // Fatura / Ödeme
        public string? InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        [Required] public string Currency { get; set; } = "TRY";
        [Required] public string PaymentMethod { get; set; } = "EFT/Havale";
        [Required] public DateTime PaymentDate { get; set; } = DateTime.Today;
        [Range(0.01, double.MaxValue)] public decimal PaymentAmount { get; set; }

        public string? Notes { get; set; }
    }
}
