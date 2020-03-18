using System.Web.Mvc;

namespace ASP.NETDesktop.Web.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            ViewBag.Title = "Home Page";
            return View();
        }
    }
}
