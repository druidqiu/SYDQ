using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SYDQ.Infrastructure.Web.Authentication
{
    public interface IExternalAuthenticationService
    {
        User GetUserDetailsFrom(string token);
    }
}
