using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SYDQ.Web.Areas.Client.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Client/Home/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
	}
}