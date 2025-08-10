using Microsoft.AspNetCore.Mvc;
using StoreProject.Common;
using StoreProject.Common.Services;
using StoreProject.Entities;
using StoreProject.Features.Product.DTOs;
using StoreProject.Features.Product.Mapper;
using StoreProject.Features.Product.Model;
using StoreProject.Features.Product.Services;

namespace StoreProject.Features.Product.Controllers
{
    [Route("Admin/Products/{action=index}")]
    public class ProductsController : BaseController
    {
        private readonly IProductManagementService _productManagementService;
        private readonly IFileManager _fileManager;
        public ProductsController(IProductManagementService productManagementService, IFileManager fileManager)
        {
            _productManagementService = productManagementService;
            _fileManager = fileManager;
        }

        public IActionResult Index(int? categoryId, string title, string slug, decimal? minPrice, decimal? maxPrice, bool isDeleted, int pageId = 1)
        {
            var parameters = new ProductFilterParamsDto()
            {
                PageId = pageId,
                Take = 6,
                CategoryId = categoryId,
                Title = title,
                Slug = slug,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                IsDeleted = isDeleted
            };
            var model = _productManagementService.GetProductsByFilter(parameters);
            return View(model);
        }

        public IActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProduct(ProductCreateModel model)
        {
            string ImageName;
            if (!ModelState.IsValid)
            {
                ErrorAlert();
                return View(model);
            }

            if(model.CategoryId == 0)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Invalid category. Please choose from the list.");
                ErrorAlert();
                return View(model);
            }

            if (model.Price < 0)
            {
                ModelState.AddModelError(nameof(model.Price), "Price must be a valid number.");
                ErrorAlert();
                return View(model);
            }

            if (model.Stock < 0)
            {
                ModelState.AddModelError(nameof(model.Stock), "Stock must be a valid number.");
                ErrorAlert();
                return View(model);
            }

            try
            {
                ImageName = _fileManager.SaveImageAndReturnImageName(model.ProductImage, Directories.ProductImage);
            }
            catch
            {
                ErrorAlert(["To change the product image, you must upload a photo."]);
                return View(model);
            }

            var result = _productManagementService.CreateProduct(new ProductCreateDto()
            {
                CategoryId = model.CategoryId,
                Title = model.Title,
                Slug = model.Slug,
                Description = model.Description,
                ImageUrl = ImageName,
                Price = model.Price,
                Stock = model.Stock
            });

            if(result.Status != OperationResultStatus.Success)
            {
                ErrorAlert(result.Message);
                return View(model);
            }

            return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }
        public IActionResult EditProduct(int id, string imageUrl)
        {
            var product = _productManagementService.GetProductBy(id);
            if(product == null)
            {
                ErrorAlert(["Product not found."]);
                return RedirectToAction("Index");
            }

            ProductEditModel model = new ProductEditModel()
            {
                Id = id,
                CategoryId = product.Category.Id,
                Title = product.Title,
                Slug = product.Slug,
                ImageUrl = imageUrl,
                Price = product.Price,
                Stock = product.Stock,
                Description = product.Description,
                IsDeleted = product.IsDeleted
            };

            TempData["CategoryId"] = model.CategoryId;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(ProductEditModel model)
        {
            if (!ModelState.IsValid)
            {
                ErrorAlert();
                return View(model);
            }

            if (model.Price < 0)
            {
                ModelState.AddModelError(nameof(model.Price), "Price must be a valid number.");
                ErrorAlert();
                return View(model);
            }

            if (model.Stock < 0)
            {
                ModelState.AddModelError(nameof(model.Stock), "Stock must be a valid number.");
                ErrorAlert();
                return View(model);
            }

            if (model.CategoryId == 0)
            {
                ModelState.AddModelError(nameof(model.CategoryId), "Invalid category. Please choose from the list.");
                ErrorAlert();
                return View(model);
            }

            int FirstCategoryId = (int)TempData["CategoryId"];
            TempData.Keep("CategoryId");
            string ProductImageUrl = model.ImageUrl;
            if (model.ProductImage != null)
                try
                {
                    ProductImageUrl = _fileManager.SaveImageAndReturnImageName(model.ProductImage, Directories.ProductImage);
                }
                catch
                {
                    ErrorAlert(["To change the product image, you must upload a photo."]);
                    return View(model);
                }

            var result = _productManagementService.EditProduct(new ProductEditDto()
            {
                Id = model.Id,
                CategoryId = (model.CategoryId == 0) ? FirstCategoryId : model.CategoryId,
                ImageUrl = ProductImageUrl,
                Title = model.Title,
                Slug = model.Slug,
                Price = model.Price,
                Stock = model.Stock,
                Description = model.Description,
                IsDeleted = model.IsDeleted
            });

            if(result.Status != OperationResultStatus.Success)
            {
                ErrorAlert(result.Message);
                return View(model);
            }

            return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }


        #region Remotes

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult IsNonNegative(decimal input)
        {
            return (input >= 0) ? Json(true) : Json("The number must be a valid number.");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult IsNonNegative(int input)
        {
            return (input >= 0) ? Json(true) : Json("The number must be a valid number.");
        }

        #endregion
    }
}
