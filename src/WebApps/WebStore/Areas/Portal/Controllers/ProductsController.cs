using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            return View(products);
        }

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
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            var vendors = await _unitOfWork.VendorRepository.GetAllAsync();
            ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
            ViewData["VendorId"] = new SelectList(vendors, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Alias,UnitDescription,Price,Discount,PreviewImage,Description,CategoryId,VendorId")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.CreatedOnUtc = DateTime.UtcNow;

                await _unitOfWork.ProductRepository.CreateAsync(product);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

			var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
			var vendors = await _unitOfWork.VendorRepository.GetAllAsync();
			ViewData["CategoryId"] = new SelectList(categories, "Id", "Name");
			ViewData["VendorId"] = new SelectList(vendors, "Id", "Name");

			return View(product);
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
