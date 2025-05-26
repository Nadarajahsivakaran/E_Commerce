using E_Commerce_API.DataAccess.IRepository;


namespace Product.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(ApplicationDbContext context) {
            Category = new CategoryRepository(context);
        }
        public ICategoryRepository Category {  get; private set; }
    }
}
