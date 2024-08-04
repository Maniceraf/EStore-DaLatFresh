using WebStore.Data;
using WebStore.Entities;
using WebStore.Interfaces.Repositories;

namespace WebStore.Implements.Repositories
{
    public class ProductImageRepository : GenericRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
