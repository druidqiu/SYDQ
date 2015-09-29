using SYDQ.IServices.Interfaces;
using SYDQ.IServices.Messaging.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SYDQ.Controllers.Controllers.Admin
{
    public class UserController : AdminBaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Client/User
        public ActionResult Index(int page = 1, string username = "", string email = "")
        {
            GetPagedUserViewRequest request = new GetPagedUserViewRequest()
            {
                PageIndex = page,
                PageSize = 1,
                Username = username,
                EmailAddress = email
            };
            var response = _userService.GetPagedUserView(request);
            
            return View(response);
        }
    }
}
