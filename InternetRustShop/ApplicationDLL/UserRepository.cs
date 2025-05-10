using ApplicationContext;
using IRepositoryDLL;
using System.Threading.Tasks;
using ApplicationDomain;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UserDLL
{
    public class UserRepository : IRepository<User>
    {
        private readonly ApplicationContext.ApplicationContext _applicationContext;

        public UserRepository(ApplicationContext.ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task AddAsync(User entity)
        {
            await _applicationContext.Users.AddAsync(entity);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            _applicationContext.Users.Remove(entity);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> FindByConditionAsync(System.Linq.Expressions.Expression<System.Func<User, bool>> predicate)
        {
            return await Task.Run(() => _applicationContext.Users.Where(predicate).ToList());
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _applicationContext.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _applicationContext.Users.FindAsync(id);
        }

        public async Task UpdateAsync(User entity, int id)
        {
            var user = await _applicationContext.Users.FindAsync(id);
            if (user != null)
            {
                user.Email = entity.Email;
                user.Role = entity.Role;
                user.Name = entity.Name;
                user.Password = entity.Password;
                user.Bin = entity.Bin;

                _applicationContext.Entry(user).State = EntityState.Modified;
                await _applicationContext.SaveChangesAsync();
            }

        }
    }
}
