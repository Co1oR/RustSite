using IRepositoryDLL;
using System.Threading.Tasks;
using ApplicationDomain;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;

namespace BinDLL
{
    public class BinRepository : IRepository<Bin>
    {
        private readonly ApplicationContext.ApplicationContext _applicationContext;

        public BinRepository(ApplicationContext.ApplicationContext applicationContext) { _applicationContext = applicationContext; }

        public async Task AddAsync(Bin entity)
        {
            await _applicationContext.Bins.AddAsync(entity);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Bin entity)
        {
            _applicationContext.Bins.Remove(entity);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Bin>> FindByConditionAsync(Expression<Func<Bin, bool>> predicate)
        {
            return await Task.Run(() => _applicationContext.Bins.Where(predicate).ToListAsync());
        }

        public async Task<IEnumerable<Bin>> GetAllAsync()
        {
            return await _applicationContext.Bins.ToListAsync();
        }

        public async Task<Bin> GetByIdAsync(int id)
        {
            return await _applicationContext.Bins.FindAsync(id);
        }

        public async Task UpdateAsync(Bin entity, int id)
        {
            var bin = await _applicationContext.Bins.FindAsync(id);
            if (bin != null)
            {
                bin.Product = entity.Product;
                bin.ProductId = entity.ProductId;
                bin.User = entity.User;
                bin.UserId = entity.UserId;

                _applicationContext.Entry(bin).State = EntityState.Modified;
                await _applicationContext.SaveChangesAsync();
            }
        }
    }
}
