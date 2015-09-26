using SYDQ.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SYDQ.Web.Areas.Client.Mappings;
using SYDQ.Core;
using SYDQ.Web.Areas.Client.Models;
using System.Web.Routing;

namespace SYDQ.Web.Areas.Client.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Client/User
        public ActionResult Index(int page = 1, string username = "", string email = "")
        {
            var users = _userService.GetPagedUsers(page, username, email).ConvertToView();
            base.RecordSearchItems(new { username = username, email = email });

            return View(users);
        }
    }
}