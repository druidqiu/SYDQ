using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
