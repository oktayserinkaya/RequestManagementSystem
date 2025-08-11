using System.ComponentModel.DataAnnotations;

namespace WEB.Areas.PurchaseTransaction.Models.PurchaseTransactionVM
{
    public class PurchaseCreateVM
    {
        // Talep özeti
        public Guid RequestId { get; set; }
        public DateTime? RequestDate { get; set; }
        public string? EmployeeFullName { get; set; }
        public string? DepartmentName { get; set; }
        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }
        public string? ProductName { get; set; }
        public decimal? RequestedAmount { get; set; }
        public string? SpecPath { get; set; }

        // Fiyatlandırma girişleri
        [Range(0, double.MaxValue)]
        public decimal? Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? UnitPrice { get; set; }

        [Required]
        public string Currency { get; set; } = "TRY";

        [Range(0, 1000)]
        public decimal DiscountRate { get; set; } = 0;

        [Range(0, 1000)]
        public decimal VatRate { get; set; } = 20;

        public DateTime? DeliveryDate { get; set; }

        // Tedarikçi / teklif
        public string? SupplierName { get; set; }
        public string? SupplierTaxNo { get; set; }

        [EmailAddress]
        public string? SupplierEmail { get; set; }

        public string? OfferNo { get; set; }
        public DateTime? OfferDate { get; set; }
        public IFormFile? OfferPdf { get; set; }
        public string? PaymentTerms { get; set; }
        public string? Notes { get; set; }

        // Hesaplanan alanlar
        public decimal? Subtotal { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? GrandTotal { get; set; }
    }
}
