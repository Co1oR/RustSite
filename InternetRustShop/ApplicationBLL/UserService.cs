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

        public async Task<bool> RegisterUser(User user)
        {

           if (await _unitOfWork.UserRepository.ValidateUser(user.Name, user.Password) && await _unitOfWork.UserRepository.CheckIfNameAvalible(user.Name))
           {
                if (_unitOfWork.UserRepository.GetAllAsync().Result.Count() == 0)
                {
                    user.Role = Role.SuperAdmin;
                }
                await _unitOfWork.UserRepository.AddAsync(user);
                return true;
           }
           else { return false; }
        }

        public async Task<bool> LoginUser(User user)
        {
            return await _unitOfWork.UserRepository.ValidateUser(user.Name, user.Password);
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

        public async Task<IEnumerable<User>> GetByConditionAsync(System.Linq.Expressions.Expression<System.Func<User, bool>> predicate)
        {
            return await _unitOfWork.UserRepository.FindByConditionAsync(predicate);
        }
    }
}
