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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext.ApplicationContext _context;
        public UnitOfWork(ApplicationContext.ApplicationContext applicationContext) 
        {
            _context = applicationContext;
        }
        public ProductRepository ProductRepository => new ProductRepository(_context);

        public UserRepository UserRepository => new UserRepository(_context);

        public BinRepository BinRepository => new BinRepository(_context);

    }
}
