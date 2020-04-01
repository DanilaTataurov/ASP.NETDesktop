using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin.Security.Cookies;

namespace ASP.NETDesktop.Web.Controllers.Api {
    public class AuthController : ApiController {
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
