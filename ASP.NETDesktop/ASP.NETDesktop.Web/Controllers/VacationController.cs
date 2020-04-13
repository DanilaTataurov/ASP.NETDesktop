using System.Linq;
using System.Web.Mvc;
using ASP.NETDesktop.Domain.Interfaces.Services;

namespace ASP.NETDesktop.Web.Controllers {
    public class VacationController : Controller {
        private readonly IVacationService _vacationService;

        public VacationController(IVacationService vacationService) {
            _vacationService = vacationService;
        }

        public ActionResult Chart() {
            return View();
        }

        public JsonResult List() {
            var vacations = _vacationService.List().Select(x => new object[] { 
                x.Developer.FirstName + " " + x.Developer.LastName, 
                null, 
                x.StartDate.ToString("dd MMM") + " - " + x.EndDate.ToString("dd MMM") + ", Status: " + x.Status, 
                x.StartDate.ToString("yyyy-MM-dd"), 
                x.EndDate.ToString("yyyy-MM-dd")
            });

            return Json(vacations, JsonRequestBehavior.AllowGet);
        }
    }
}
