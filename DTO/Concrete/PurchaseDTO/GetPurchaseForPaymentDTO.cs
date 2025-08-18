using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.PurchaseDTO
{
    public class GetPurchaseForPaymentDTO
    {
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

        // Pricing (UI için decimal? uygundur)
        public decimal? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal DiscountRate { get; set; }
        public decimal VatRate { get; set; }
        public decimal? Subtotal { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? GrandTotal { get; set; }
        public string Currency { get; set; } = "TRY";

        public string? OfferPdfPath { get; set; }
    }
}
