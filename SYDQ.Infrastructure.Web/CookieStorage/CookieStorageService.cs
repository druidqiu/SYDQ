﻿using System;
using System.Web;

namespace SYDQ.Infrastructure.Web.CookieStorage
{
    public class CookieStorageService : ICookieStorageService
    {
        public void Save(string key, string value, DateTime expires)
        {
            HttpContext.Current.Response.Cookies[key].Value = value;
            HttpContext.Current.Response.Cookies[key].Expires = expires;
        }

        public string Retrieve(string key)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
            return cookie != null ? cookie.Value : "";
        }
    }

}
