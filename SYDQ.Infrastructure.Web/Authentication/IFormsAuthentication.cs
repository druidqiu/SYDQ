using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Infrastructure.Web.Authentication
{
    public interface IFormsAuthentication
    {
        void SetAuthenticationToken(string token);
        string GetAuthenticationToken();
        void SignOut();
        string LoginUrl { get; }
    } 
}
