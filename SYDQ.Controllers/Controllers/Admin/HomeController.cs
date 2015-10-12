using System.Web.Mvc;

namespace SYDQ.Controllers.Controllers.Admin
{
    public class HomeController : AdminBaseController
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
