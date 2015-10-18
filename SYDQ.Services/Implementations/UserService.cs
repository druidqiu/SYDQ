using System.Linq;
using SYDQ.Core;
using SYDQ.Infrastructure.Domain;
using SYDQ.Infrastructure.Pager;
using SYDQ.Infrastructure.UnitOfWork;
using SYDQ.IServices.Interfaces;
using SYDQ.IServices.Messaging.UserService;
using SYDQ.IServices.ViewModels;

namespace SYDQ.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IRepository<User> userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public UserView Authenticate(string username, string password)
        {
            return _userRepository.GetEntity(u => u.Username == username && u.Password == password).ConvertToUserView();
        }


        public UserView GetUserById(int id)
        {
            return _userRepository.GetEntity(id).ConvertToUserView();
        }

        public IPagedList<UserView> GetPagedUserView(GetPagedUserViewRequest request)
        {
            var query = _userRepository.GetAllIncludeAsNoTracking(u => u.Roles);

            if (!string.IsNullOrWhiteSpace(request.Username))
                query = query.Where(u => u.Username.Contains(request.Username));
            if (!string.IsNullOrWhiteSpace(request.EmailAddress))
                query = query.Where(u => u.EmailAddress.Contains(request.EmailAddress));

            return query.ToPagedList(request.PageIndex, request.PageSize, request.SortField, request.SortOrder, p => p.ConvertToUserView());
        }
    }
}
