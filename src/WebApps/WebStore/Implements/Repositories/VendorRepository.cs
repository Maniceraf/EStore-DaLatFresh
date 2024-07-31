using WebStore.Data;
using WebStore.Entities;
using WebStore.Interfaces.Repositories;

namespace WebStore.Implements.Repositories
{
    public class VendorRepository : GenericRepository<Vendor>, IVendorRepository
    {
        public VendorRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
