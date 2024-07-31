using WebStore.Data;
using WebStore.Entities;
using WebStore.Interfaces.Repositories;

namespace WebStore.Implements.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
