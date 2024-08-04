using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client.Extensions.Msal;
using System.Globalization;
using WebStore.Areas.Portal.Models.Product;
using WebStore.Entities;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;


namespace WebStore.Areas.Portal.Controllers
{
    [Route("portal/products")]
    public class ProductsController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFirebaseStorageService _firebaseStorageService;

        public ProductsController(
            IUnitOfWork unitOfWork,
            IFirebaseStorageService firebaseStorageService)
        {
            _unitOfWork = unitOfWork;
            _firebaseStorageService = firebaseStorageService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            var result = products.OrderByDescending(x => x.CreatedOnUtc).Select(x =>
            {
                var a = _firebaseStorageService.GetSignedUrlAsync(x.PreviewImage ?? string.Empty).Result;
                return new ProductResponModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    OrignalPrice = (x.Price ?? 0).ToString("#,###", cul.NumberFormat),
                    Discount = x.Discount,
                    LastPrice = ((x.Price ?? 0) - ((x.Price ?? 0) * x.Discount)).ToString("#,###", cul.NumberFormat),
                    PreviewImage = a,
                    ProductType = x.ProductType.Name,
                    Vendor = x.Vendor.Name,
                    ViewCounts = x.ViewCounts,
                };
            });

            return View(result);
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
        public async Task<IActionResult> Create([Bind("Name,Alias,UnitDescription,Price,Discount,ImageFiles,Description,ProductTypeId,VendorId")] CreateProductRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var product = new Product
                {
                    Name = model.Name,
                    Alias = model.Alias,
                    Description = model.Description,
                    UnitDescription = model.UnitDescription,
                    Price = model.Price,
                    Discount = model.Discount,
                    ViewCounts = 0,
                    CreatedOnUtc = DateTime.UtcNow,
                    ProductTypeId = model.ProductTypeId,
                    VendorId = model.VendorId
                };

                await _unitOfWork.ProductRepository.CreateAsync(product);
                await _unitOfWork.SaveChangesAsync();

                var images = new List<ProductImage>();
                foreach (var file in model.ImageFiles)
                {
                    if (file.Length > 0)
                    {
                        var extension = Path.GetExtension(file.FileName);
                        var fileName = $"{Guid.NewGuid()}{extension}";
                        var result = await _firebaseStorageService.UploadFileAsync(file, fileName);
                        if (result)
                        {
                            images.Add(new ProductImage
                            {
                                Name = fileName,
                                ProductId = product.Id
                            });
                            product.PreviewImage = fileName;
                        }
                    }
                }

                await _unitOfWork.ProductImageRepository.CreateRangeAsync(images);
                await _unitOfWork.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            var productTypes = await _unitOfWork.ProductTypeRepository.GetAllAsync();
            var vendors = await _unitOfWork.VendorRepository.GetAllAsync();
            ViewData["ProductTypeId"] = new SelectList(productTypes, "Id", "Name");
            ViewData["VendorId"] = new SelectList(vendors, "Id", "Name");

            return View(model);
        }

        [HttpGet]
        [Route("{id}/edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var request = new EditProductRequestModel
            {
                Id = product.Id,
                Name = product.Name,
                Alias = product.Alias ?? string.Empty,
                Description = product.Description ?? string.Empty,
                Discount = product.Discount,
                Price = product.Price ?? 0,
                ProductTypeId = product.ProductTypeId,
                UnitDescription = product.UnitDescription ?? string.Empty,
                VendorId = product.VendorId
            };

            var productTypes = await _unitOfWork.ProductTypeRepository.GetAllAsync();
            var vendors = await _unitOfWork.VendorRepository.GetAllAsync();
            ViewData["ProductTypeId"] = new SelectList(productTypes, "Id", "Name");
            ViewData["VendorId"] = new SelectList(vendors, "Id", "Name");

            return View(request);
        }

        [HttpPost]
        [Route("{id}/edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Alias,UnitDescription,Price,Discount,ImageFile,Description,ProductTypeId,VendorId")] EditProductRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound();
                }

                product.Name = model.Name;
                product.Alias = model.Alias;
                product.Description = model.Description;
                product.UnitDescription = model.UnitDescription;
                product.Price = model.Price;
                product.Discount = model.Discount;
                product.UpdatedOnUtc = DateTime.UtcNow;
                product.ProductTypeId = model.ProductTypeId;
                product.VendorId = model.VendorId;

                if (model.ImageFile != null)
                {
                    var isDeleted = await _firebaseStorageService.DeleteFileAsync(product.PreviewImage ?? string.Empty);
                    if (isDeleted)
                    {
                        var extension = Path.GetExtension(model.ImageFile.FileName);
                        var fileName = $"{Guid.NewGuid()}{extension}";
                        var result = await _firebaseStorageService.UploadFileAsync(model.ImageFile, fileName);
                        if (result)
                        {
                            product.PreviewImage = fileName;
                        }
                    }
                }

                await _unitOfWork.ProductRepository.UpdateAsync(product);
                await _unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var productTypes = await _unitOfWork.ProductTypeRepository.GetAllAsync();
            var vendors = await _unitOfWork.VendorRepository.GetAllAsync();
            ViewData["ProductTypeId"] = new SelectList(productTypes, "Id", "Name");
            ViewData["VendorId"] = new SelectList(vendors, "Id", "Name");

            return View(model);
        }

        [HttpGet]
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

        [HttpPost]
        [Route("{id}/delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product != null)
            {
                await _unitOfWork.ProductRepository.DeleteAsync(product);
            }

            await _unitOfWork.SaveChangesAsync();

            await _firebaseStorageService.DeleteFileAsync(product.PreviewImage);

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _unitOfWork.ProductRepository.GetAll().Any(e => e.Id == id);
        }
    }
}