using WebStore.Entities;

namespace WebStore.Interfaces.Repositories
{
    public interface IProductImageRepository : IGenericRepository<ProductImage>
    {
        Task<List<ProductImage>> GetImagesAsync(int productId);
    }
}
