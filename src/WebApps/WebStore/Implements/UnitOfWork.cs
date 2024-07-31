using Microsoft.EntityFrameworkCore;
using WebStore.Interfaces;
using WebStore.Interfaces.Repositories;

namespace WebStore.Implements
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        public IVendorRepository VendorRepository { get; }

        public UnitOfWork(
            DbContext context,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            IVendorRepository vendorRepository)
        {
            Context = context;
            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
            VendorRepository = vendorRepository;
        }

        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
