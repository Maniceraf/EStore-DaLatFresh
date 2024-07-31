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

		public IActionResult Detail(int id)
		{
			var data = _unitOfWork.ProductRepository.GetById(id);
			if (data == null)
			{
				TempData["Message"] = $"Không thấy sản phẩm có mã {id}";
				return Redirect("/404");
			}

			var result = new ProductDetailViewModel
			{
				Id = data.Id,
				Name = data.Name,
				Price = data.Price ?? 0,
				Description = data.Description ?? string.Empty,
				PreviewImage = data.PreviewImage ?? string.Empty,
				ShortDescription = data.UnitDescription ?? string.Empty,
				Category = data.Category.Name,
				RemainsCount = 10,//tính sau
				Rate = 5,//check sau
			};
			return View(result);
		}

		public IActionResult Search(string? query)
		{
			var products = _unitOfWork.ProductRepository.GetAll();

			if (query != null)
			{
				products = products.Where(p => p.Name.Contains(query)).ToList();
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
