using System.Web;
using System.Web.Security;

namespace SYDQ.Infrastructure.Web.Authentication
{
    public class AspFormsAuthentication : IFormsAuthentication
    {
        public void SetAuthenticationToken(string token)
        {
            FormsAuthentication.SetAuthCookie(token, false);
        }

        public string GetAuthenticationToken()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public string LoginUrl
        {
            get { return FormsAuthentication.LoginUrl; }
        }
    }

}
