using System;
using System.Threading.Tasks;
using AutoMapper;
using CORE.Entities.Concrete;
using CORE.Interface;
using DTO.Concrete.PurchaseDTO;
using BUSINESS.Manager.Interface;

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

        public async Task<GetPurchaseForPaymentDTO?> GetForPaymentAsync(Guid requestId)
        {
            var purchase = await _repo.GetByDefaultAsync(x => x.RequestId == requestId);
            return purchase is null ? null : _mapper.Map<GetPurchaseForPaymentDTO>(purchase);
        }

        public async Task<bool> UpsertAsync(CreateOrUpdatePurchaseDTO dto)
        {
            var existing = await _repo.GetByDefaultAsync(x => x.RequestId == dto.RequestId);
            if (existing is null)
            {
                var entity = _mapper.Map<Purchase>(dto);
                entity.RequestId = dto.RequestId;
                return await _repo.AddAsync(entity);
            }
            else
            {
                existing.SupplierName = dto.SupplierName;
                existing.SupplierTaxNo = dto.SupplierTaxNo;
                existing.SupplierIban = dto.SupplierIban;
                existing.SupplierEmail = dto.SupplierEmail;
                existing.SupplierPhone = dto.SupplierPhone;

                // Quantity int? — DTO da int? olmalı; eğer decimal? ise map’te int? cevir
                existing.Quantity = dto.Quantity;
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
    }
}
