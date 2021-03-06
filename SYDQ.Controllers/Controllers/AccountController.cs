﻿using System.Web.Mvc;
using SYDQ.Controllers.ViewModel;
using SYDQ.Infrastructure.Web.Authentication;
using SYDQ.Infrastructure.Web.Mvc.Session;
using SYDQ.IServices.Interfaces;

namespace SYDQ.Controllers.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFormsAuthentication _formAuthentication;
        public AccountController(IUserService userService, IFormsAuthentication formAuthentication)
        {
            _userService = userService;
            _formAuthentication = formAuthentication;
        }

        [AllowAnonymous]
        public ActionResult Index(string returnUrl)
        {
            return RedirectToAction("Login", "Account");
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            Response.Cache.SetNoStore();

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToLocal(returnUrl);
            }
            ViewBag.ReturnUrl = returnUrl;

            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var authenticateUser = _userService.Authenticate(model.Username, model.Password);

                if (authenticateUser != null)
                {
                    _formAuthentication.SetAuthenticationToken(authenticateUser.Username);
                    return RedirectToLocal(returnUrl);
                }
            }

            ModelState.AddModelError("", "User name or password is wrong.");
            ViewBag.ShowError = true;
            return View(model);
        }

        public ActionResult LogOff()
        {
            Response.Cache.SetNoStore();
            _formAuthentication.SignOut();
            SessionHelper.EmptySession();
            return Redirect(_formAuthentication.LoginUrl);
        }


        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home", new { area = "Client" });
            }
        }
    }
}
