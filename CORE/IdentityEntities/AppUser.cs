using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CORE.Enums;
using Microsoft.AspNetCore.Identity;

namespace CORE.IdentityEntities
{
    public class AppUser : IdentityUser<Guid>
    {
        private DateTime _createdDate = DateTime.Now;
        private Status _status = Status.Active;

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateOnly Birthdate { get; set; }
        public DateTime CreatedDate { get => _createdDate; set => _createdDate = value; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get => _status; set => _status = value; }
    }
}
