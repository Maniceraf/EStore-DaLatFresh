using WebStore.Data;
using WebStore.Entities;
using WebStore.Interfaces.Repositories;

namespace WebStore.Implements.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
