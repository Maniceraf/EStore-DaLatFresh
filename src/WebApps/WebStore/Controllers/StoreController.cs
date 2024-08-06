using Microsoft.AspNetCore.Mvc;
using System.Globalization;
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
        public IActionResult Products(List<int> categoryId, List<int> vendorId, int? minPrice, int? maxPrice, int length = 10, string sortByPrice = "asc")
        {
            var products = _unitOfWork.ProductRepository.GetAll();

            if (categoryId.Count > 0 || vendorId.Count > 0)
            {
                products = products.Where(p => 
					(categoryId.Count == 0 || categoryId.Contains(p.ProductType.CategoryId)
					&& (vendorId.Count == 0 || vendorId.Contains(p.VendorId)))).ToList();
            }

			if (minPrice.HasValue && maxPrice.HasValue)
			{
				products = products.Where(x => x.Price >= minPrice.Value && x.Price < maxPrice.Value).ToList();
			}

			if (sortByPrice == "asc")
			{
				products = products.OrderBy(x => x.Price).ToList();
			}
			else if (sortByPrice == "desc")
			{
				products = products.OrderByDescending(x => x.Price).ToList();
			}
			else
			{
				products = products.OrderBy(x => x.Price).ToList();
			}

			products = products.Take(length).ToList();

			CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
			var result = products.Select(x =>
            {
                var a = _firebaseStorageService.GetSignedUrlAsync(x.PreviewImage ?? string.Empty).Result;
                return new ProductViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = (x.Price ?? 0).ToString("#,###", cul.NumberFormat),
                    PreviewImage = a ?? "",
                    Description = x.UnitDescription ?? "",
                    Category = x.ProductType.Name
                };
            });

            return View(result);
        }

		[HttpGet]
		[Route("products/{id}")]
		public IActionResult Detail(int id)
		{
			var data = _unitOfWork.ProductRepository.GetById(id);
			if (data == null)
			{
				TempData["Message"] = $"Không thấy sản phẩm có mã {id}";
				return Redirect("/404");
			}

			CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
			var a = _firebaseStorageService.GetSignedUrlAsync(data.PreviewImage ?? string.Empty).Result;
			var result = new ProductDetailViewModel
			{
				Id = data.Id,
				Name = data.Name,
				Price = (data.Price ?? 0).ToString("#,###", cul.NumberFormat),
				Description = data.Description ?? string.Empty,
				PreviewImage = a,
				ShortDescription = data.UnitDescription ?? string.Empty,
				Category = data.ProductType.Name,
				RemainsCount = 10,
				Rate = 5,
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

			CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
			var result = products.Select(p => new ProductViewModel
			{
				Id = p.Id,
				Name = p.Name,
				Price = (p.Price ?? 0).ToString("#,###", cul.NumberFormat),
				PreviewImage = p.PreviewImage ?? "",
				Description = p.UnitDescription ?? "",
				Category = p.ProductType.Name
			});

			return View(result);
		}
	}
}