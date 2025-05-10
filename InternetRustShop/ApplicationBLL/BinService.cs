using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationDomain;
using UnitOfWorkDLL;

namespace BinBLL
{
    public class BinService : IBinService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BinService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddProductToBinAsync(int userId, int productId)
        {
            if (_unitOfWork.BinRepository.FindByConditionAsync(x=> x.UserId == userId && x.ProductId == productId) == null)
            {
                var BinModel = new Bin
                {
                    UserId = userId,
                    ProductId = productId
                };
                await _unitOfWork.BinRepository.AddAsync(BinModel);
            }
        }

        public async Task<IEnumerable<Product>> GetUserBinAsync(int userId)
        {
            var binItems = await _unitOfWork.BinRepository.FindByConditionAsync(x => x.UserId == userId);
            if (binItems != null)
            {
                return binItems.Select(x=>x.Product).ToList();
            }
            else
            {
                return null;
            }
        }

        public async Task RemoveProductFromBinAsync(int userId, int productId)
        {
            if (_unitOfWork.BinRepository.FindByConditionAsync(x => x.UserId == userId && x.ProductId == productId) != null)
            {
                var binModel = new Bin
                {
                    UserId = userId,
                    ProductId = productId
                };
                await _unitOfWork.BinRepository.DeleteAsync(binModel);
            }
        }
    }
}
