using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces;
using WebStore.ViewModels;

namespace WebStore.NewFolder
{
	public class MenuViewComponent : ViewComponent
	{
		private readonly IUnitOfWork _unitOfWork;

		public MenuViewComponent(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

		public IViewComponentResult Invoke()
		{
			var data = _unitOfWork.CategoryRepository.GetAll();
			var result = data.Select(x => new MenuCategoryViewModel
			{
				Id = x.Id,
				Name = x.Name,
				Count = x.ProductTypes.SelectMany(x => x.Products).Count()
			}).OrderBy(p => p.Id);

			return View(result);
		}
	}
}
