using SYDQ.Core;
using SYDQ.Infrastructure.Domain;
using SYDQ.Infrastructure.Pager;
using SYDQ.Infrastructure.UnitOfWork;
using SYDQ.IServices.Interfaces;
using SYDQ.IServices.Messaging;
using SYDQ.IServices.Messaging.UserService;
using SYDQ.IServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            return query.OrderBy(u => u.Id)
                .ToPagedList(request.PageIndex, request.PageSize, p => p.ConvertToUserView());
        }
    }
}
