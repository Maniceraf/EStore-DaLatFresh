using Microsoft.EntityFrameworkCore;
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

        public async Task<List<ProductImage>> GetImagesAsync(int productId)
        {
            var result = await Context.ProductImages.Where(x => x.ProductId == productId).ToListAsync();
            return result;
        }
    }
}
