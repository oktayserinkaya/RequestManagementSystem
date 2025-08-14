using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Concrete.PurchaseDTO;

namespace BUSINESS.Manager.Interface
{
    public interface IPurchaseManager
    {
        Task<bool> UpsertAsync(CreateOrUpdatePurchaseDTO dto);
        Task<GetPurchaseForPaymentDTO?> GetForPaymentAsync(Guid requestId);
    }
}
