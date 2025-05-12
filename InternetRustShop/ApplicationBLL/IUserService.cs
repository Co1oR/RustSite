using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationDomain;

namespace UserBLL
{
    public interface IUserService
    {
        Task<bool> RegisterUser(User user);
        Task<bool> LoginUser(User user);
        Task DeleteAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<IEnumerable<User>> GetByConditionAsync(System.Linq.Expressions.Expression<System.Func<User, bool>> predicate);
        Task<User> GetByIdAsync(int id);
        Task UpdateAsync(User user, int id);
    }
}
