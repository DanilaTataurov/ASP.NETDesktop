using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ASP.NETDesktop.Domain.Entities.Base;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASP.NETDesktop.Domain.Identity {
    public class User : IdentityUser<Guid, UserLogin, UserRole, UserClaim>, IEntity<Guid> {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User, Guid> manager, string authenticationType) {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }
}
