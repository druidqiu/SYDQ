using SYDQ.Core;
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
        User Authenticate(string username, string password);
    }
}
