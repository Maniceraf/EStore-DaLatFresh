using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebStore.Areas.Portal.Models.ProductTypeModels;
using WebStore.Entities;
using WebStore.Interfaces;

namespace WebStore.Areas.Portal.Controllers
{
    [Route("portal/product-types")]
    public class ProductTypesController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var productTypes = await _unitOfWork.ProductTypeRepository.GetAllAsync();
            var result = productTypes.OrderByDescending(x => x.CreatedOnUtc).ToList();
            return View(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var productType = await _unitOfWork.ProductTypeRepository.GetByIdAsync(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        [HttpGet]
        [Route("create")]
        public async Task<IActionResult> Create()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmCreate([Bind("Name,Alias,Description,CategoryId")] CreateProductTypeRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var productType = new ProductType
                {
                    Name = model.Name,
                    Alias = model.Alias,
                    Description = model.Description,
                    CategoryId = model.CategoryId,
                    CreatedOnUtc = DateTime.UtcNow
                };

                await _unitOfWork.ProductTypeRepository.CreateAsync(productType);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            return View(model);
        }

        [HttpGet]
        [Route("{id}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var productType = await _unitOfWork.ProductTypeRepository.GetByIdAsync(id);
            if (productType == null)
            {
                return NotFound();
            }

            var request = new EditProductTypeRequestModel
            {
                Id = productType.Id,
                Name = productType.Name,
                Alias = productType.Alias,
                Description = productType.Description,
                CategoryId = productType.CategoryId
            };

            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            return View(request);
        }

        [HttpPost]
        [Route("{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmEdit(int id, [Bind("Name,Alias,Description,CategoryId")] EditProductTypeRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var productType = await _unitOfWork.ProductTypeRepository.GetByIdAsync(id);
                if (productType == null)
                {
                    return NotFound();
                }

                productType.Name = model.Name;
                productType.Alias = model.Alias;
                productType.Description = model.Description;
                productType.CategoryId = model.CategoryId;
                productType.UpdatedOnUtc = DateTime.UtcNow;

                await _unitOfWork.ProductTypeRepository.UpdateAsync(productType);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            return View(model);
        }

        [HttpGet]
        [Route("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var productType = await _unitOfWork.ProductTypeRepository.GetByIdAsync(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        [HttpPost]
        [Route("{id}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productType = await _unitOfWork.ProductTypeRepository.GetByIdAsync(id);
            if (productType != null)
            {
                await _unitOfWork.ProductTypeRepository.DeleteAsync(productType);
            }
            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}