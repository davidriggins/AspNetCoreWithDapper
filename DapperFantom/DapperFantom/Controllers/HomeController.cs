using DapperFantom.Entities;
using DapperFantom.Helpers;
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
        private readonly IServiceProvider service;

        public HomeController(IServiceProvider serviceProvider)
        {
            categoryService = serviceProvider.GetRequiredService<CategoryService>();
            articleService = serviceProvider.GetRequiredService<ArticleService>();
            service = serviceProvider;
        }

        public IActionResult Index()
        {
            //List<Category> categories = categoryService.GetAll();
            //List<Article> articles = articleService.GetHome();

            int page = HttpContext.Request.Query["page"].Count == 0 ? 1 : Int32.Parse(HttpContext.Request.Query["page"]);
            PaginationHelpers paginationHelpers = new PaginationHelpers(service);
            PaginationModel paginationModel = paginationHelpers.ArticlePagination(page);

            GeneralViewModel model = new GeneralViewModel
            {
                //CategoryList = categories,
                //ArticleList = articles
                PaginationModel = paginationModel,
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

        [Route("SearchResults")]
        public IActionResult Search()
        {
            string searchQuery = HttpContext.Request.Query["q"];
            List<Article> articles = articleService.Search(searchQuery);
            GeneralViewModel model = new GeneralViewModel 
            { 
                ArticleList = articles 
            };

            return View(model);
        }
    }
}