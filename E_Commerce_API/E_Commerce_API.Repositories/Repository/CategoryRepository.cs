using E_Commerce_API.DataAccess.IRepository;
using Product_API.DataAccess.Repository;
using Product_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_API.DataAccess.Repository
{
    public class CategoryRepository(ApplicationDbContext dbContext) : GenericRepository<Category>(dbContext), ICategoryRepository
    {
    }
}
