using System;
using System.Threading.Tasks;
using AutoMapper;
using BUSINESS.Manager.Interface;
using CORE.Entities.Concrete;
using CORE.Interface;
using DTO.Concrete.PurchaseDTO;

namespace BUSINESS.Manager.Concrete
{
    public class PurchaseManager : IPurchaseManager
    {
        private readonly IBaseRepository<Purchase> _repo;
        private readonly IMapper _mapper;

        public PurchaseManager(IBaseRepository<Purchase> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<bool> UpsertAsync(CreateOrUpdatePurchaseDTO dto)
        {
           
            int? qtyInt = dto.Quantity.HasValue
                ? (int?)Convert.ToInt32(
                        Math.Round(
                            Convert.ToDecimal(dto.Quantity.Value),
                            0,
                            MidpointRounding.AwayFromZero))
                : null;

            var existing = await _repo.GetByDefaultAsync(x => x.RequestId == dto.RequestId);
            if (existing is null)
            {
                var entity = _mapper.Map<Purchase>(dto);
                entity.RequestId = dto.RequestId;

               
                entity.Quantity = qtyInt;

                return await _repo.AddAsync(entity);
            }
            else
            {
                existing.SupplierName = dto.SupplierName;
                existing.SupplierTaxNo = dto.SupplierTaxNo;
                existing.SupplierIban = dto.SupplierIban;
                existing.SupplierEmail = dto.SupplierEmail;
                existing.SupplierPhone = dto.SupplierPhone;

               
                existing.OfferNo = dto.OfferNo;
                existing.OfferDate = dto.OfferDate;
                existing.PaymentTerms = dto.PaymentTerms;
                existing.Notes = dto.Notes;
                existing.DeliveryDate = dto.DeliveryDate;

               
                existing.Quantity = qtyInt;             
                existing.UnitPrice = dto.UnitPrice;
                existing.DiscountRate = dto.DiscountRate;
                existing.VatRate = dto.VatRate;
                existing.Subtotal = dto.Subtotal;
                existing.DiscountAmount = dto.DiscountAmount;
                existing.VatAmount = dto.VatAmount;
                existing.GrandTotal = dto.GrandTotal;
                existing.Currency = dto.Currency;
                existing.OfferPdfPath = dto.OfferPdfPath;

                return await _repo.UpdateAsync(existing);
            }
        }

        public async Task<GetPurchaseForPaymentDTO?> GetForPaymentAsync(Guid requestId)
        {
            var p = await _repo.GetByDefaultAsync(x => x.RequestId == requestId);
            if (p is null) return null;

            return new GetPurchaseForPaymentDTO
            {
                SupplierName = p.SupplierName,
                SupplierTaxNo = p.SupplierTaxNo,
                SupplierIban = p.SupplierIban,
                SupplierEmail = p.SupplierEmail,
                SupplierPhone = p.SupplierPhone,

                OfferNo = p.OfferNo,
                OfferDate = p.OfferDate,
                PaymentTerms = p.PaymentTerms,
                Notes = p.Notes,
                DeliveryDate = p.DeliveryDate,

               
                Quantity = p.Quantity.HasValue ? (decimal?)p.Quantity.Value : null,
                UnitPrice = p.UnitPrice,
                DiscountRate = p.DiscountRate,
                VatRate = p.VatRate,
                Subtotal = p.Subtotal,
                DiscountAmount = p.DiscountAmount,
                VatAmount = p.VatAmount,
                GrandTotal = p.GrandTotal,
                Currency = p.Currency,
                OfferPdfPath = p.OfferPdfPath
            };
        }
    }
}
