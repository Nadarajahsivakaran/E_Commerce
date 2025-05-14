using E_Commerce_API.DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_API.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ApplicationDbContext context) {
            Category = new CategoryRepository(context);
        }
        public ICategoryRepository Category {  get; private set; }
    }
}
