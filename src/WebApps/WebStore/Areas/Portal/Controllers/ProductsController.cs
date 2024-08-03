using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebStore.Areas.Portal.Models.Product;
using WebStore.Entities;
using WebStore.Interfaces;


namespace WebStore.Areas.Portal.Controllers
{
    [Route("portal/products")]
	public class ProductsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            return View(products);
        }

        [HttpGet]
        [Route("{id}")]
		public async Task<IActionResult> Details(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

		[Route("create")]
		public async Task<IActionResult> Create()
        {
            var productTypes = await _unitOfWork.ProductTypeRepository.GetAllAsync();
            var vendors = await _unitOfWork.VendorRepository.GetAllAsync();
            ViewData["ProductTypeId"] = new SelectList(productTypes, "Id", "Name");
            ViewData["VendorId"] = new SelectList(vendors, "Id", "Name");

            return View();
        }

        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Alias,UnitDescription,Price,Discount,PreviewImage,Description,ProductTypeId,VendorId")] CreateProductRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = model.Name,
                    Alias = model.Alias,
                    Description = model.Description,
                    UnitDescription = model.UnitDescription,
                    PreviewImage = model.PreviewImage,
                    Price = model.Price,
                    Discount = model.Discount,
                    ViewCounts = 0,
                    CreatedOnUtc = DateTime.UtcNow,
                    ProductTypeId = model.ProductTypeId,
                    VendorId = model.VendorId
                };

                await _unitOfWork.ProductRepository.CreateAsync(product);

                try
                {
                    await _unitOfWork.SaveChangesAsync();

                }
                catch (Exception ex)
                {
                    int a = 1;
                }

                return RedirectToAction(nameof(Index));
            }

            var productTypes = await _unitOfWork.ProductTypeRepository.GetAllAsync();
            var vendors = await _unitOfWork.VendorRepository.GetAllAsync();
            ViewData["ProductTypeId"] = new SelectList(productTypes, "Id", "Name");
            ViewData["VendorId"] = new SelectList(vendors, "Id", "Name");

			return View(model);
        }

		[Route("{id}/edit")]
		public async Task<IActionResult> Edit(int id)
        {
			var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
			if (product == null)
			{
				return NotFound();
			}

			var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
			var vendors = await _unitOfWork.VendorRepository.GetAllAsync();
			ViewData["CategoryId"] = new SelectList(categories, "Id", "Id");
			ViewData["VendorId"] = new SelectList(vendors, "Id", "Id");

			return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Alias,UnitDescription,Price,PreviewImage,ProductionDateOnLocal,ProductionDateOnUtc,Discount,ViewCounts,Description,CreatedOnLocal,CreatedOnUtc,UpdatedOnLocal,UpdatedOnUtc,CategoryId,VendorId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork.ProductRepository.UpdateAsync(product);
                    await _unitOfWork.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

			var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
			var vendors = await _unitOfWork.VendorRepository.GetAllAsync();
			ViewData["CategoryId"] = new SelectList(categories, "Id", "Id");
			ViewData["VendorId"] = new SelectList(vendors, "Id", "Id");

			return View(product);
        }

		[Route("{id}/delete")]
		public async Task<IActionResult> Delete(int id)
        {
			var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
			if (product == null)
			{
				return NotFound();
			}

			return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
			var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
			if (product != null)
            {
                await _unitOfWork.ProductRepository.DeleteAsync(product);
            }

            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _unitOfWork.ProductRepository.GetAll().Any(e => e.Id == id);
        }
    }
}
