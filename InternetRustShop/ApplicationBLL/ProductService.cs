using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using ApplicationDomain;
using UnitOfWorkDLL;

namespace ProductBLL
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(Product product)
        {
            if (_unitOfWork.ProductRepository.GetByIdAsync(product.Id) == null)
            {
                await _unitOfWork.ProductRepository.AddAsync(product);
            }
        }

        public async Task DeleteAsync(Product product)
        {
            if (_unitOfWork.ProductRepository.GetByIdAsync(product.Id) != null)
            {
                await _unitOfWork.ProductRepository.DeleteAsync(product);
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _unitOfWork.ProductRepository.GetAllAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _unitOfWork.ProductRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Product product, int id)
        {
            var oldProduct = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (oldProduct != null)
            {
                await _unitOfWork.ProductRepository.UpdateAsync(product, id);
            }
        }
    }
}
