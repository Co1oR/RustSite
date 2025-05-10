using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationDomain;
using UnitOfWorkDLL;
using UserDLL;

namespace UserBLL
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(User user)
        {

           if (_unitOfWork.UserRepository.GetByIdAsync(user.Id) == null)
            {
                await _unitOfWork.UserRepository.AddAsync(user);
            }
        }

        public async Task DeleteAsync(User user)
        {
            if (_unitOfWork.UserRepository.GetByIdAsync(user.Id) == null)
            {
                await _unitOfWork.UserRepository.DeleteAsync(user);
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _unitOfWork.UserRepository.GetAllAsync();
        }

        public Task<User> GetByIdAsync(int id)
        {
            var user = _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user == null) { return null; }
            else { return user; }
        }

        public async Task UpdateAsync(User user, int id)
        {
            var oldUser = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (oldUser != null)
            {
                await _unitOfWork.UserRepository.UpdateAsync(user, id);
            }
        }
    }
}
