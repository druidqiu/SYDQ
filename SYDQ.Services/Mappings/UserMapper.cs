using SYDQ.Core;
using SYDQ.IServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Services
{
    public static class UserMapper
    {
        public static UserView ConvertToUserView(this User user)
        {
            if (user == null)
                return null;
            
            UserView userView = new UserView()
            {
                Id = user.Id,
                Username = user.Username,
                EmailAddress = user.EmailAddress,
                Roles = String.Join(",", user.Roles.Select(r => r.Name))
            };
            return userView;
        }

        public static List<UserView> ConvertToUserView(this IEnumerable<User> users)
        {
            if (users == null)
                return null;

            List<UserView> userViewList = new List<UserView>();
            foreach (User user in users)
            {
                userViewList.Add(user.ConvertToUserView());
            }

            return userViewList;
        }
    }
}
