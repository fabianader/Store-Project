using Microsoft.AspNetCore.Mvc;
using StoreProject.Features.Category.Mapper;
using StoreProject.Features.Shared;
using StoreProject.Features.Shared.Model;

namespace StoreProject.Features.Shared.Components.Tree
{
    public class Tree : ViewComponent
    {
        private readonly ICategoryManagementService _categoryManagementService;
        public Tree(ICategoryManagementService categoryManagementService)
        {
            _categoryManagementService = categoryManagementService;
        }

        public IViewComponentResult Invoke()
        {
            var model = _categoryManagementService.GetExistedCategories()
                .Where(category => category.ParentId == null)
                .Select(category => new CategoryTreeModel()
                {
                    Title = category.Title,
                    Slug = category.Slug,
                    ChildCategories = _categoryManagementService.GetChildCategories(category.Id)
                        .Select(c => new ChildCategory(){ Title = c.Title, Slug = c.Slug }).ToList()
                }).ToList();
            
            return View("Tree", model);
        }
    }
}
