using DapperFantom.Entities;
using DapperFantom.Models;
using DapperFantom.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DapperFantom.Controllers
{
    public class HomeController : Controller
    {
        private readonly CategoryService categoryService;
        private readonly ArticleService articleService;

        public HomeController(IServiceProvider serviceProvider)
        {
            categoryService = serviceProvider.GetRequiredService<CategoryService>();
            articleService = serviceProvider.GetRequiredService<ArticleService>();
        }

        public IActionResult Index()
        {
            List<Category> categories = categoryService.GetAll();
            List<Article> articles = articleService.GetHome();

            GeneralViewModel model = new GeneralViewModel
            {
                CategoryList = categories,
                ArticleList = articles
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}