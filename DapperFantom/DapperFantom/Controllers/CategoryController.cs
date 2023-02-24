using DapperFantom.Entities;
using DapperFantom.Helpers;
using DapperFantom.Models;
using DapperFantom.Services;
using Microsoft.AspNetCore.Mvc;

namespace DapperFantom.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryService categoryService;
        private ArticleService articleService;
        private IServiceProvider serviceProvider;


        public CategoryController(IServiceProvider serviceProvider)
        {
            categoryService = serviceProvider.GetRequiredService<CategoryService>();
            articleService = serviceProvider.GetRequiredService<ArticleService>();
        }


        [Route("/Category/{slug}/{page?}")]
        public IActionResult Index(string slug, int page = 1)
        {
            if (slug != null)
            {
                Category category = categoryService.GetBySlug(slug);
                if (category != null)
                {
                    List<Category> categories = categoryService.GetAll();
                    //List<Article> articles = articleService.GetByCategoryId(category.CategoryId);

                    PaginationHelpers paginationHelper = new PaginationHelpers(serviceProvider);
                    PaginationModel paginationModel = paginationHelper.CategoryPagination(category, page);


                    GeneralViewModel model = new GeneralViewModel
                    {
                        CategoryList = categories,
                        PaginationModel = paginationModel,
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
