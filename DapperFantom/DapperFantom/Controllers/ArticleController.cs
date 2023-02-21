using DapperFantom.Entities;
using DapperFantom.Models;
using DapperFantom.Services;
using Microsoft.AspNetCore.Mvc;

namespace DapperFantom.Controllers
{

    public class ArticleController : Controller
    {
        private CategoryService categoryService;

        public ArticleController(IServiceProvider serviceProvider)
        {
            categoryService = serviceProvider.GetRequiredService<CategoryService>();
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Add()
        {
            List<Category> categories = categoryService.GetAll();

            GeneralViewModel model = new GeneralViewModel
            {
                CategoryList= categories,
            };

            return View(model);
        }
    }
}
