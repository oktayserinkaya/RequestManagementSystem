using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DTO.Concrete.PurchaseDTO
{
    public class CreateOrUpdatePurchaseDTO
    {
        // Request kimliği
        public Guid RequestId { get; set; }

        // Talep özeti
        public DateTime? RequestDate { get; set; }
        public string? EmployeeFullName { get; set; }
        public string? DepartmentName { get; set; }
        public decimal? RequestedAmount { get; set; }

        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }

        // Katalog ürünü adı (varsa)
        public string? ProductName { get; set; }

        // ÖZEL: Listede olmayan ürün adı (CreateRequest’ten gelir)
        public string? SpecialProductName { get; set; }

        // Şartname yolu (pdf)
        public string? SpecPath { get; set; }

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

        // Şartlar/Notlar
        public string? PaymentTerms { get; set; }
        public string? Notes { get; set; }
        public DateTime? DeliveryDate { get; set; }

        // Fiyatlandırma
        public decimal? Quantity { get; set; }          // View’da decimal? kullanılmış
        public decimal? UnitPrice { get; set; }
        public decimal DiscountRate { get; set; } = 0m;
        public decimal VatRate { get; set; } = 20m;
        public string Currency { get; set; } = "TRY";

        // Hesap sonuçları
        public decimal? Subtotal { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? GrandTotal { get; set; }
    }
}
