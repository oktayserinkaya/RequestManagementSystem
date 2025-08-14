using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Entities.Abstract;

namespace CORE.Entities.Concrete
{
    public class Purchase : BaseEntity
    {
        public Guid RequestId { get; set; }
        public Request? Request { get; set; }

        // Tedarikçi
        public string? SupplierName { get; set; }
        public string? SupplierTaxNo { get; set; }
        public string? SupplierIban { get; set; }
        public string? SupplierEmail { get; set; }
        public string? SupplierPhone { get; set; }

        // Fiyatlandırma
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal DiscountRate { get; set; }   // %
        public decimal VatRate { get; set; }        // %
        public decimal? Subtotal { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? GrandTotal { get; set; }
        public string Currency { get; set; } = "TRY";

        // Dosyalar
        public string? OfferPdfPath { get; set; }
    }
}
