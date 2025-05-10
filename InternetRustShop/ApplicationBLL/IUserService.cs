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
        Task AddAsync(User user);
        Task DeleteAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task UpdateAsync(User user, int id);
    }
}
