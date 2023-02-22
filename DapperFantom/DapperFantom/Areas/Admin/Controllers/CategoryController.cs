using DapperFantom.Areas.Admin.Models;
using DapperFantom.Models;
using DapperFantom.Services;
using Microsoft.AspNetCore.Mvc;

namespace DapperFantom.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    public class CategoryController : Controller
    {
        private CategoryService categoryService;
        public CategoryController(IServiceProvider serviceProvider)
        {
            categoryService = serviceProvider.GetRequiredService<CategoryService>();
        }


        public IActionResult Index()
        {
            List<Entities.Category> categoryList = categoryService.GetAll();

            var model = new UserViewModel
            {
                CategoryList = categoryList,
            };

            return View(model);
        }
    }
}
