using SYDQ.Infrastructure.Pager;
using SYDQ.IServices.Messaging;
using SYDQ.IServices.Messaging.UserService;
using SYDQ.IServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SYDQ.IServices.Interfaces
{
    public interface IUserService
    {
        UserView GetUserById(int id);
        UserView Authenticate(string username, string password);
        IPagedList<UserView> GetPagedUserView(GetPagedUserViewRequest request);
    }
}
