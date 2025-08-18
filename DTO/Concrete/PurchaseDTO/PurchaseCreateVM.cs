using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace DTO.Concrete.PurchaseDTO
{
    public class PurchaseCreateVM
    {
        // Request özet
        public Guid RequestId { get; set; }
        [Display(Name = "Talep Tarihi")] public DateTime RequestDate { get; set; }
        [Display(Name = "Personel Adı Soyadı")] public string? EmployeeFullName { get; set; }
        [Display(Name = "Departman")] public string? DepartmentName { get; set; }
        [Display(Name = "Kategori")] public string? CategoryName { get; set; }
        [Display(Name = "Alt Kategori")] public string? SubCategoryName { get; set; }
        [Display(Name = "Ürün Adı")] public string? ProductName { get; set; }
        [Display(Name = "Talep Adedi")] public decimal? RequestedAmount { get; set; }
        public string? SpecPath { get; set; }
        public string? SpecialProductName { get; set; }

        // Supplier
        [Display(Name = "Tedarikçi Adı")] public string? SupplierName { get; set; }
        [Display(Name = "Vergi No")] public string? SupplierTaxNo { get; set; }
        [Display(Name = "IBAN")] public string? SupplierIban { get; set; }
        [Display(Name = "E-Posta")][EmailAddress] public string? SupplierEmail { get; set; }
        [Display(Name = "Telefon")] public string? SupplierPhone { get; set; }

        // Offer meta (EKLENDİ)
        [Display(Name = "Teklif No")] public string? OfferNo { get; set; }
        [Display(Name = "Teklif Tarihi")] public DateTime? OfferDate { get; set; }
        [Display(Name = "Ödeme / Teslimat Şartları")] public string? PaymentTerms { get; set; }
        [Display(Name = "Notlar")] public string? Notes { get; set; }
        [Display(Name = "Teslim Tarihi")] public DateTime? DeliveryDate { get; set; }

        // Pricing (UI tarafında decimal? rahat)
        [Display(Name = "Miktar")] public decimal? Quantity { get; set; }
        [Display(Name = "Birim Fiyat")] public decimal? UnitPrice { get; set; }
        [Display(Name = "İndirim Oranı (%)")] public decimal DiscountRate { get; set; }
        [Display(Name = "KDV Oranı (%)")] public decimal VatRate { get; set; }
        [Display(Name = "Ara Toplam")] public decimal? Subtotal { get; set; }
        [Display(Name = "İndirim Tutarı")] public decimal? DiscountAmount { get; set; }
        [Display(Name = "KDV Tutarı")] public decimal? VatAmount { get; set; }
        [Display(Name = "Genel Toplam")] public decimal? GrandTotal { get; set; }
        [Display(Name = "Para Birimi")] public string Currency { get; set; } = "TRY";

        [Display(Name = "Teklif PDF")] public IFormFile? OfferPdf { get; set; }
        public string? OfferPdfPath { get; set; }
    }
}
