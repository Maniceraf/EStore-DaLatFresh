using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces;
using WebStore.ViewModels;

namespace WebStore.ViewComponents
{
	public class FilterViewComponent : ViewComponent
	{
		private readonly IUnitOfWork _unitOfWork;

		public FilterViewComponent(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

		public IViewComponentResult Invoke()
		{
			var categories = _unitOfWork.CategoryRepository.GetAll();
			var vendors = _unitOfWork.VendorRepository.GetAll();
			var result = new FilterViewModel
			{
				Categories = categories.Select(x => new CategoryFilterViewModel
				{
					Id = x.Id,
					Name = x.Name
				}).OrderBy(p => p.Id).ToList(),
				Vendors = vendors.Select(x => new VendorFilterViewModel
				{
					Id = x.Id,
					Name = x.Name
				}).OrderBy(p => p.Id).ToList(),
			};

			return View(result);
		}
	}
}
