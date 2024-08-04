using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
		private readonly IFirebaseStorageService _firebaseStorageService;

        public StoreController(IUnitOfWork unitOfWork, IFirebaseStorageService firebaseStorageService)
        {
            _unitOfWork = unitOfWork;
			_firebaseStorageService = firebaseStorageService;
        }

        public IActionResult Index()
        {
            return View();
        }

		[HttpGet]
		[Route("products")]
        public IActionResult Products(List<int> categoryId, List<int> vendorId)
        {
            var products = _unitOfWork.ProductRepository.GetAll();

            if (categoryId.Count > 0)
            {
                products = products.Where(p => 
					categoryId.Contains(p.ProductType.CategoryId)
					&& vendorId.Contains(p.VendorId)).ToList();
            }

            var result = products.OrderByDescending(x => x.CreatedOnUtc).Select(x =>
            {
                var a = _firebaseStorageService.GetSignedUrlAsync(x.PreviewImage ?? string.Empty).Result;
                return new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price ?? 0,
                    PreviewImage = a ?? "",
                    Description = x.UnitDescription ?? "",
                    Category = x.ProductType.Name
                };
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
				Category = data.ProductType.Name,
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
				Category = p.ProductType.Name
			});

			return View(result);
		}
	}
}