using DapperFantom.Entities;
using DapperFantom.Models;
using DapperFantom.Services;
using Microsoft.AspNetCore.Mvc;

namespace DapperFantom.ViewComponents
{
    public class RightSide : ViewComponent
    {
        private CategoryService categoryService;
        private ArticleService articleService;

        public RightSide(IServiceProvider serviceProvider)
        {
            categoryService = serviceProvider.GetRequiredService<CategoryService>();
            articleService = serviceProvider.GetRequiredService<ArticleService>();
        }


        public IViewComponentResult Invoke()
        {
            List<Category> categories = categoryService.GetAll();
            List<Article> articles = articleService.GetAll().Take(4).OrderByDescending(x => x.Hit).ToList();

            GeneralViewModel model = new GeneralViewModel
            {
                CategoryList = categories,
                ArticleList = articles
            };

            return View(model);
        }
    }
}
