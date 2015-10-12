using System;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using SYDQ.Infrastructure.Configuration;

namespace SYDQ.Infrastructure.Web.Authentication
{
    public class JanrainAuthenticationService : IExternalAuthenticationService
    {
        public User GetUserDetailsFrom(string token)
        {
            User user = new User();

            string parameters = String.Format("apiKey={0}&token={1}&format=xml",
                  ApplicationSettingsFactory.GetApplicationSettings().JanrainApiKey,
                                                                          token);
            string response;
            using (var w = new WebClient())
            {
                response = w.UploadString("https://rpxnow.com/api/v2/auth_info",
                                                                       parameters);
            }
            var xmlResponse = XDocument.Parse(response);
            var userProfile = (from x in xmlResponse.Descendants("profile")
                let xElement = x.Element("identifier")
                where xElement != null
                select new
                               {
                                   id = xElement.Value,
                                   email = (string)x.Element("email") ?? "No Email"
                               }).SingleOrDefault();

            if (userProfile != null)
            {
                user.AuthenticationToken = userProfile.id;
                user.Email = userProfile.email;
                user.IsAuthenticated = true;
            }
            else
                user.IsAuthenticated = false;

            return user;
        }
    }

}
