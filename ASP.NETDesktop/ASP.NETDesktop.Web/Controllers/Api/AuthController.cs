using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.Cookies;

namespace ASP.NETDesktop.Web.Controllers.Api {
    public class AuthController : ApiController {
        public AuthController() { }
        private NETUserManager _userManager;

        public NETUserManager UserManager {
            get => _userManager ?? Request.GetOwinContext().GetUserManager<NETUserManager>();
            private set => _userManager = value;
        }

        [Route("api/Auth/Logout")]
        public async Task<IHttpActionResult> LogoutAsync() {
            try {
                if (User.Identity.IsAuthenticated) {
                    Request.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
                }
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
