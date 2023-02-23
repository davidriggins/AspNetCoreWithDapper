using DapperFantom.Entities;
using DapperFantom.Models;
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


        public IActionResult Index(string slug)
        {
            if (slug != null)
            {
                Category category = categoryService.GetBySlug(slug);
                if (category != null)
                {
                    List<Category> categories = categoryService.GetAll();
                    List<Article> articles = articleService.GetCategory(category.CategoryId);

                    GeneralViewModel model = new GeneralViewModel
                    {
                        CategoryList = categories,
                        ArticleList= articles,
                        Category = category
                    };

                    return View(model);
                }
                else
                {
                    return Redirect("/");
                }
            }
            else
            {
                return Redirect("/");
            }
        }
    }
}
