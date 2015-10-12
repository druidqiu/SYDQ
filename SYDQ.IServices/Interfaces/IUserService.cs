using SYDQ.Infrastructure.Pager;
using SYDQ.IServices.Messaging.UserService;
using SYDQ.IServices.ViewModels;

namespace SYDQ.IServices.Interfaces
{
    public interface IUserService
    {
        UserView GetUserById(int id);
        UserView Authenticate(string username, string password);
        IPagedList<UserView> GetPagedUserView(GetPagedUserViewRequest request);
    }
}
