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
        public IProductImageRepository ProductImageRepository { get; }

        public UnitOfWork(
            ApplicationDbContext context,
            ICategoryRepository categoryRepository,
            IProductRepository productRepository,
            IVendorRepository vendorRepository,
            IProductTypeRepository productTypeRepository,
            IProductImageRepository productImageRepository)
        {
            Context = context;
            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
            VendorRepository = vendorRepository;
            ProductTypeRepository = productTypeRepository;
            ProductImageRepository = productImageRepository;
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
