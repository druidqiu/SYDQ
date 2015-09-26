using SYDQ.Core;
using SYDQ.Infrastructure.Pager;
using SYDQ.Web.Areas.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYDQ.Web.Areas.Client.Mappings
{
    public static class UserMapping
    {
        public static UserViewModel ConvertToView(this User user)
        {
            if (user == null)
                return null;

            UserViewModel userView = new UserViewModel()
            {
                Id = user.Id,
                Username = user.Username,
                EmailAddress = user.EmailAddress,
                Roles = String.Join(",", user.Roles.Select(r => r.Name))
            };

            return userView;

        }

        public static UserListViewModel ConvertToView(this IPagedList<User> pagedUsers)
        {
            if (pagedUsers == null)
                return null;

            UserListViewModel userView = new UserListViewModel();
            userView.PagerMetaData = pagedUsers.GetMetaData();
            foreach (var pUser in pagedUsers)
            {
                userView.Users.Add(pUser.ConvertToView());
            }

            return userView;
        }
    }
}