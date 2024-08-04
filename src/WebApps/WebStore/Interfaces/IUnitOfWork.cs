using Microsoft.EntityFrameworkCore;
using WebStore.Interfaces.Repositories;

namespace WebStore.Interfaces
{
    public interface IUnitOfWork
    {
        DbContext Context { get; }
        Task SaveChangesAsync();
        void SaveChanges();
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        IVendorRepository VendorRepository { get; }
        IProductTypeRepository ProductTypeRepository { get; }
        IProductImageRepository ProductImageRepository { get; }
    }
}
