using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE.Enums
{
    public enum Status
    {
        [Display(Name = "Aktif")]
        Active = 1,

        [Display(Name = "Güncellenmiş")]
        Modified,

        [Display(Name = "Pasif")]
        Passive,

        [Display(Name = "Beklemede")]
        Pending,

        [Display(Name = "Onaylandı")]
        Approved,

        [Display(Name = "Reddedildi")]
        Rejected,

        [Display(Name = "Tamamlanmış")]
        Completed,

        [Display(Name = "Ödeme Bekleniyor")]
        WaitingPayment,

        [Display(Name = "Ödendi")]
        Paid
    }
}
