using System.ComponentModel.DataAnnotations;

namespace WEB.Areas.PaymentTransaction.Models.PaymentTransactionVM
{
    public class PaymentFormVM
    {
        // Request
        public Guid RequestId { get; set; }
        public DateTime? RequestDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? EmployeeFullName { get; set; }
        public string? DepartmentName { get; set; }
        public string? ProductName { get; set; }
        public decimal? RequestedAmount { get; set; }
        public string? SpecPath { get; set; }

        // Supplier
        public string? SupplierName { get; set; }
        public string? SupplierTaxNo { get; set; }
        public string? SupplierIban { get; set; }
        public string? SupplierEmail { get; set; }
        public string? SupplierPhone { get; set; }

        // Offer meta
        public string? OfferNo { get; set; }
        public DateTime? OfferDate { get; set; }
        public string? PaymentTerms { get; set; }
        public string? Notes { get; set; }
        public DateTime? DeliveryDate { get; set; }

        // Pricing (UI hesapları için decimal?)
        public decimal? Quantity { get; set; }     // <<< BURASI decimal? OLDU
        public decimal? UnitPrice { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal VatRate { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? GrandTotal { get; set; }
        public string Currency { get; set; } = "TRY";

        // Invoice/Payment
        public string? InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string PaymentMethod { get; set; } = "EFT/Havale";
        public DateTime PaymentDate { get; set; } = DateTime.Today;
        public decimal PaymentAmount { get; set; }
    }
}
