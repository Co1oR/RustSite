using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinDLL;
using ProductDLL;
using UserDLL;

namespace UnitOfWorkDLL
{
    public interface IUnitOfWork
    {
        ProductRepository ProductRepository { get; }
        UserRepository UserRepository { get; }
        BinRepository BinRepository { get; }
    }
}
