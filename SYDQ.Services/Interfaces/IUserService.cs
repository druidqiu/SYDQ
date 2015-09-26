using SYDQ.Core;
using SYDQ.Infrastructure.Pager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.Services.Interfaces
{
    public interface IUserService
    {
        User GetUserById(int id);
        void AddUser(User user);
        List<User> GetAllUsers();
        IPagedList<User> GetPagedUsers(int page,string username, string emailAddress);
        User Authenticate(string username, string password);
    }
}
