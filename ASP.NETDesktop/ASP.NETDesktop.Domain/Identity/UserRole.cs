using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASP.NETDesktop.Domain.Identity {
    public class UserRole : IdentityUserRole<Guid> {
        public virtual Role Role { get; set; }
    }
}
