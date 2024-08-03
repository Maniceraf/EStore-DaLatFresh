using Microsoft.AspNetCore.Mvc;
using WebStore.Areas.Portal.Models.CategoryModels;
using WebStore.Entities;
using WebStore.Interfaces;

namespace WebStore.Areas.Portal.Controllers
{
    [Route("portal/categories")]
    public class CategoriesController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            var result = categories.OrderByDescending(x => x.Id);
            return View(categories);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpGet]
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmCreate([Bind("Name,Alias,Description")] CreateCategoryRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = model.Name,
                    Alias = model.Alias,
                    Description = model.Description,
                    PreviewImage = string.Empty,
                    CreatedOnLocal = DateTime.Now,
                    CreatedOnUtc = DateTime.UtcNow
                };

                await _unitOfWork.CategoryRepository.CreateAsync(category);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        [Route("{id}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var request = new EditCategoryRequestModel
            {
                Id = category.Id,
                Name = category.Name,
                Alias = category.Alias,
                Description = category.Description,
            };

            return View(request);
        }

        [HttpPost]
        [Route("{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmEdit(int id,[Bind("Id,Name,Alias,Description")] EditCategoryRequestModel model)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                category.Name = model.Name;
                category.Alias = model.Alias;
                category.Description = model.Description;
                category.UpdatedOnLocal = DateTime.Now;
                category.UpdatedOnUtc = DateTime.UtcNow;

                await _unitOfWork.CategoryRepository.UpdateAsync(category);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        [Route("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [Route("{id}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category != null)
            {
                await _unitOfWork.CategoryRepository.DeleteAsync(category);
            }

            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
