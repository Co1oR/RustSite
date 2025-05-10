using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationDomain;

namespace BinBLL
{
    public interface IBinService
    {
        Task AddProductToBinAsync(int userId, int productId);
        Task RemoveProductFromBinAsync(int userId, int productId);
        Task<IEnumerable<Product>> GetUserBinAsync(int userId);
    }
}
