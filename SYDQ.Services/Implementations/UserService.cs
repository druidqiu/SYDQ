using SYDQ.Core;
using SYDQ.Infrastructure.Domain;
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
            return _userRepository.GetAllIncludeAsNoTracking("Roles,Messages").ToList();
        }

        public User Authenticate(string username, string password)
        {
            return _userRepository.GetEntity(u => u.Username == username && u.Password == password);
        }
    }
}
