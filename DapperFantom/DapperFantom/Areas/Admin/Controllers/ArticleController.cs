using DapperFantom.Areas.Admin.Models;
using DapperFantom.Entities;
using DapperFantom.Models;
using DapperFantom.Services;
using Microsoft.AspNetCore.Mvc;

namespace DapperFantom.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    public class ArticleController : Controller
    {
        private ArticleService articleService;

        public ArticleController(IServiceProvider serviceProvider)
        {
            articleService = serviceProvider.GetRequiredService<ArticleService>();
        }


        public IActionResult Index()
        {
            List<Article> articleList = articleService.GetAll();
            UserViewModel model = new UserViewModel
            {
                ArticleList = articleList
            };

            return View(model);
        }
    }
}
