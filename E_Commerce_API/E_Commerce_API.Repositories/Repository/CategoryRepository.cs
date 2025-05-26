using E_Commerce_API.DataAccess.IRepository;
using Product.Models;
using Product_API.DataAccess.Repository;

namespace Product.DataAccess.Repository
{
    public class CategoryRepository(ApplicationDbContext dbContext) : GenericRepository<Category>(dbContext), ICategoryRepository
    {
    }
}
