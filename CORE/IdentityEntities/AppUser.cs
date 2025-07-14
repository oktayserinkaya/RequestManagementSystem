using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CORE.IdentityEntities
{
    public class AppUser : IdentityUser<Guid>
    {
        public bool HasFirstPasswordChanged { get; set; }
    }
}
