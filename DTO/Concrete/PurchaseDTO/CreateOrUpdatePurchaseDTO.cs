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
        public Guid RequestId { get; set; }

      
        public DateTime? RequestDate { get; set; }
        public string? EmployeeFullName { get; set; }
        public string? DepartmentName { get; set; }
        public decimal? RequestedAmount { get; set; }

        public string? CategoryName { get; set; }
        public string? SubCategoryName { get; set; }

       
        public string? ProductName { get; set; }

        
        public string? SpecialProductName { get; set; }

       
        public string? SpecPath { get; set; }

     
        public string? SupplierName { get; set; }
        public string? SupplierTaxNo { get; set; }
        public string? SupplierIban { get; set; }
        public string? SupplierEmail { get; set; }
        public string? SupplierPhone { get; set; }

 
        public string? OfferNo { get; set; }
        public DateTime? OfferDate { get; set; }
        public string? OfferPdfPath { get; set; }
        public IFormFile? OfferPdf { get; set; }

        
        public string? PaymentTerms { get; set; }
        public string? Notes { get; set; }
        public DateTime? DeliveryDate { get; set; }

        
        public decimal? Quantity { get; set; }          
        public decimal? UnitPrice { get; set; }
        public decimal DiscountRate { get; set; } = 0m;
        public decimal VatRate { get; set; } = 20m;
        public string Currency { get; set; } = "TRY";

      
        public decimal? Subtotal { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? GrandTotal { get; set; }
    }
}
