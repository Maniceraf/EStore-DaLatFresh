using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(int? categoryId)
        {
            var products = _unitOfWork.ProductRepository.GetAll();

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.Id == categoryId.Value).ToList();
            }

            var result = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price ?? 0,
                PreviewImage = p.PreviewImage ?? "",
                Description = p.UnitDescription ?? "",
                Category = p.Category.Name
            });

            return View(result);
        }
    }
}
