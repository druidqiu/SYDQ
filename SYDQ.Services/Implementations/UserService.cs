using SYDQ.Core;
using SYDQ.Infrastructure.Domain;
using SYDQ.Infrastructure.Pager;
using SYDQ.Infrastructure.UnitOfWork;
using SYDQ.Services.Interfaces;
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

        public User GetUserById(int id)
        {
            return _userRepository.GetEntity(id);
        }

        public void AddUser(User user)
        {
            _userRepository.Insert(user);
            _unitOfWork.Commit();
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllIncludeAsNoTracking(u => u.Roles, u => u.Messages).ToList();
        }

        public IPagedList<User> GetPagedUsers(int page, string username, string emailAddress)
        {
            var query = _userRepository.GetAllIncludeAsNoTracking(u => u.Roles);
            if (!string.IsNullOrWhiteSpace(username))
            {
                query = query.Where(u => u.Username.Contains(username));
            }
            if (!string.IsNullOrWhiteSpace(emailAddress))
            {
                query = query.Where(u => u.EmailAddress.Contains(emailAddress));
            }
            return query.OrderBy(u => u.Id).ToPagedList(page, 1);
        }

        public User Authenticate(string username, string password)
        {
            return _userRepository.GetEntity(u => u.Username == username && u.Password == password);
        }
    }
}
