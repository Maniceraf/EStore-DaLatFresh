using Microsoft.EntityFrameworkCore;
using WebStore.Data;
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
        public IProductTypeRepository ProductTypeRepository { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            IVendorRepository vendorRepository,
            IProductTypeRepository productTypeRepository)
        {
            Context = context;
            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
            VendorRepository = vendorRepository;
            ProductTypeRepository = productTypeRepository;
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
