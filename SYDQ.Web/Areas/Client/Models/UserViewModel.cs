using SYDQ.Infrastructure.Pager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SYDQ.Web.Areas.Client.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string Roles { get; set; }
    }

    public class UserListViewModel : PagedViewModel
    {
        public UserListViewModel()
        {
            Users = new List<UserViewModel>();
        }
        public List<UserViewModel> Users { get; set; }
    }
}