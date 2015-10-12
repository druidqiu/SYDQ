using System;

namespace SYDQ.Infrastructure.Web.Mvc.Session
{
    [Serializable]
    public class PagePermission
    {
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}
