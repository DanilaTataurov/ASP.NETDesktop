using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ASP.NETDesktop.Domain.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using ASP.NETDesktop.DAL.Context;
using ASP.NETDesktop.BLL.Identity;

namespace ASP.NETDesktop.Web {
    public class NETUserManager : UserManager<User, Guid> {
        public NETUserManager(IUserStore<User, Guid> store)
            : base(store) { }

        public override Task<IdentityResult> CreateAsync(User user) {
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;
            return base.CreateAsync(user);
        }

        public static NETUserManager Create(IdentityFactoryOptions<NETUserManager> options, IOwinContext context) {
            var manager = new NETUserManager(new ApplicationUserStore(context.Get<NETContext>()));
            manager.UserValidator = new UserValidator<User, Guid>(manager) {
                AllowOnlyAlphanumericUserNames = false, RequireUniqueEmail = true
            };
            manager.PasswordValidator = new PasswordValidator {
                RequireNonLetterOrDigit = false, RequireDigit = false, RequireLowercase = false, RequireUppercase = false
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null) {
                manager.UserTokenProvider = new DataProtectorTokenProvider<User, Guid>(dataProtectionProvider.Create("ASP.NET Identity")) {
                    TokenLifespan = TimeSpan.FromHours(4)
                };
            }
            return manager;
        }
    }

    public class TestSignInManager : SignInManager<User, Guid> {
        public TestSignInManager(NETUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager) { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(User user) {
            return user.GenerateUserIdentityAsync((NETUserManager) UserManager, DefaultAuthenticationTypes.ApplicationCookie);
        }

        public static TestSignInManager Create(IdentityFactoryOptions<TestSignInManager> options, IOwinContext context) {
            return new TestSignInManager(context.GetUserManager<NETUserManager>(), context.Authentication);
        }
    }
}
