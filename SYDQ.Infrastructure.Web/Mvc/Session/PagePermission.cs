using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Infrastructure.Web.Mvc.Session
{
    [Serializable]
    public class PagePermission
    {
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}
