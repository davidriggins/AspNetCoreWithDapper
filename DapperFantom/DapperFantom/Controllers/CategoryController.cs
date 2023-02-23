using DapperFantom.Services;
using Microsoft.AspNetCore.Mvc;

namespace DapperFantom.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService categoryService;
        private ArticleService articleService;


        public CategoryController(IServiceProvider serviceProvider)
        {
            categoryService = serviceProvider.GetRequiredService<CategoryService>();
            articleService = serviceProvider.GetRequiredService<ArticleService>();
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
