using Microsoft.AspNetCore.Mvc;
using StoreProject.Common;
using StoreProject.Features.Category.DTOs;
using StoreProject.Features.Category.Mapper;
using StoreProject.Features.Category.Model;
using StoreProject.Features.Category.Services;
using StoreProject.Features.Shared.Model;

namespace StoreProject.Features.Category.Controllers
{
    [Route("Admin/CategoriesManagement/{action=index}")]
    public class CategoriesManagementController : BaseController
    {
        private readonly ICategoryManagementService _categoryManagementService;
        public CategoriesManagementController(ICategoryManagementService categoryManagementService)
        {
            _categoryManagementService = categoryManagementService;
        }

        public IActionResult Index(bool showDeletedCategories = false)
        {
            ViewBag.ShowDeletedCategories = showDeletedCategories;

            var categories = (showDeletedCategories == false) ? _categoryManagementService.GetExistedCategories() : _categoryManagementService.GetDeletedCategories();
            var model = categories.Select(category => CategoryMapper.MapCategoryDtoToCategoryModel(category)).ToList();
            return View(model);
        }

        public IActionResult CreateCategory()
        {
            return PartialView("_AdminCreateCategory");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(CategoryCreateModel model)
        {
            ViewBag.Layout = string.Empty;

            if (!ModelState.IsValid)
            {
                ViewBag.Layout = "_AdminLayout";
                return PartialViewAndShowErrorAlert(PartialView("_AdminCreateCategory", model));
            }

            var result = _categoryManagementService.CreateCategory(new CreateCategoryDto()
            {
                Title = model.Title,
                Slug = model.Slug,
                ParentId = (model.ParentId == 0) ? null : model.ParentId
            });

            if (result.Status != OperationResultStatus.Success)
            {
                ViewBag.Layout = "_AdminLayout";
                return PartialViewAndShowErrorAlert(PartialView("_AdminCreateCategory", model), result.Message);
            }

            return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }

        public IActionResult EditCategory(int id)
        {
            var category = _categoryManagementService.GetCategoryBy(id);
            if (category == null)
            {
                ErrorAlert(["Category not found."]);
                return RedirectToAction("Index");
            }

            CategoryEditModel model = new CategoryEditModel()
            {
                Id = id,
                Title = category.Title,
                Slug = category.Slug,
                ParentId = category.ParentId,
                IsDeleted = category.IsDeleted
            };

            return PartialView("_AdminEditCategory", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(CategoryEditModel model)
        {
            ViewBag.Layout = string.Empty;

            if (!ModelState.IsValid)
            {
                ViewBag.Layout = "_AdminLayout";
                return PartialViewAndShowErrorAlert(PartialView("_AdminEditCategory", model));
            }

            var result = _categoryManagementService.EditCategory(new EditCategoryDto()
            {
                Id = model.Id,
                Title = model.Title,
                Slug = model.Slug,
                ParentId = (model.ParentId == 0) ? null : model.ParentId,
                IsDeleted = model.IsDeleted
            });

            if (result.Status != OperationResultStatus.Success)
            {
                ViewBag.Layout = "_AdminLayout";
                return PartialViewAndShowErrorAlert(PartialView("_AdminEditCategory", model), result.Message);
            }

            return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }

        public IActionResult CategoryTree()
        {
            var model = _categoryManagementService.GetExistedCategories()
                .Where(category => category.ParentId == null)
                .Select(category => CategoryMapper.MapCategoryDtoToCategoryModel(category)).ToList();

            return ViewComponent("CategoryTree", model);
        }
    }
}
