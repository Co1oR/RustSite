using IRepositoryDLL;
using System.Threading.Tasks;
using ApplicationDomain;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using Microsoft.EntityFrameworkCore;

namespace ProductDLL
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly ApplicationContext.ApplicationContext _applicationContext;

        public ProductRepository(ApplicationContext.ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task AddAsync(Product entity)
        {
            await _applicationContext.Products.AddAsync(entity);
            await _applicationContext.SaveChangesAsync();

        }

        public async Task DeleteAsync(Product entity)
        {
            _applicationContext.Products.Remove(entity);
            await _applicationContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> FindByConditionAsync(Expression<Func<Product, bool>> predicate)
        {
            return await Task.Run(() => _applicationContext.Products.Where(predicate).ToListAsync());
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _applicationContext.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _applicationContext.Products.FindAsync(id);
        }

        public async Task UpdateAsync(Product entity, int id)
        {
            var product = await _applicationContext.Products.FindAsync(id);
            if (product != null)
            {
                product.Name = entity.Name;
                product.Description = entity.Description;
                product.Cost = entity.Cost;
                product.Bin = entity.Bin;

                _applicationContext.Entry(product).State = EntityState.Modified;
                await _applicationContext.SaveChangesAsync();
            }
        }
    }
}
