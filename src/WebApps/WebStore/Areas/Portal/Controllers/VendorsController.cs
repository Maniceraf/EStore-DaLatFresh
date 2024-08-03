using Microsoft.AspNetCore.Mvc;
using WebStore.Areas.Portal.Models.VendorModels;
using WebStore.Entities;
using WebStore.Interfaces;

namespace WebStore.Areas.Portal.Controllers
{
    [Route("portal/vendors")]
    public class VendorsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;

        public VendorsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var vendors = await _unitOfWork.VendorRepository.GetAllAsync();
            var result = vendors.OrderByDescending(x => x.CreatedOnUtc).ToList();
            return View(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }
            return View(vendor);
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
        public async Task<IActionResult> ConfirmCreate([Bind("Name,Email,PhoneNumber,Address")] CreateVendorRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var vendor = new Vendor
                {
                    Name = model.Name,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Address = model.Address,
                    CreatedOnUtc = DateTime.UtcNow
                };

                await _unitOfWork.VendorRepository.CreateAsync(vendor);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        [Route("{id}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }

            var request = new EditVendorRequestModel
            {
                Id = vendor.Id,
                Name = vendor.Name,
                Email = vendor.Email,
                Address = vendor.Address,
                PhoneNumber = vendor.PhoneNumber  
            };

            return View(request);
        }

        [HttpPost]
        [Route("{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Email,PhoneNumber,Address")] EditVendorRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(id);
                if (vendor == null)
                {
                    return NotFound();
                }

                vendor.Name = model.Name;
                vendor.Email = model.Email;
                vendor.Address = model.Address;
                vendor.PhoneNumber = model.PhoneNumber;
                vendor.UpdatedOnUtc = DateTime.UtcNow;

                await _unitOfWork.VendorRepository.UpdateAsync(vendor);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        [Route("{id}/delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }
            return View(vendor);
        }

        [HttpPost]
        [Route("{id}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendor = await _unitOfWork.VendorRepository.GetByIdAsync(id);
            if (vendor != null)
            {
                await _unitOfWork.VendorRepository.DeleteAsync(vendor);
            }

            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
