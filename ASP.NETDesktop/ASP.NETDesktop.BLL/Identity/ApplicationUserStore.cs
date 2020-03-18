using System;
using ASP.NETDesktop.DAL.Context;
using ASP.NETDesktop.Domain.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASP.NETDesktop.BLL.Identity {
    public class ApplicationUserStore : UserStore<User, Role, Guid, UserLogin, UserRole, UserClaim> {
        public ApplicationUserStore(NETContext context) : base(context) { }
    }
}
