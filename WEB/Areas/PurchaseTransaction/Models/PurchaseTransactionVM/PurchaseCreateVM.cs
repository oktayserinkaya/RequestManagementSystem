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
        public decimal? RequestedAmount { get; set; }

        // Kategori/Ürün
        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }
        public string? ProductName { get; set; }            // katalog ürün adı
        public string? SpecialProductName { get; set; }     // listede olmayan ürün adı (EKLENDİ)
        public string? SpecPath { get; set; }               // şartname pdf yolu

        // Tedarikçi
        public string? SupplierName { get; set; }
        public string? SupplierTaxNo { get; set; }
        public string? SupplierIban { get; set; }
        public string? SupplierEmail { get; set; }
        public string? SupplierPhone { get; set; }

        // Teklif
        public string? OfferNo { get; set; }
        public DateTime? OfferDate { get; set; }
        public string? OfferPdfPath { get; set; }
        public IFormFile? OfferPdf { get; set; }

        public string? PaymentTerms { get; set; }
        public string? Notes { get; set; }
        public DateTime? DeliveryDate { get; set; }

        // Fiyatlandırma (View tarafında decimal? tutuyoruz)
        public decimal? Quantity { get; set; }              // VM: decimal?
        public decimal? UnitPrice { get; set; }
        public decimal DiscountRate { get; set; } = 0m;
        public decimal VatRate { get; set; } = 20m;
        public string Currency { get; set; } = "TRY";

        // Hesap özet
        public decimal? Subtotal { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? GrandTotal { get; set; }
    }
}
