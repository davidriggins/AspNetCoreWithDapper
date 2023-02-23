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
        private CategoryService categoryService;
        private CityService cityService;

        public ArticleController(IServiceProvider serviceProvider)
        {
            articleService = serviceProvider.GetRequiredService<ArticleService>();
            categoryService = serviceProvider.GetRequiredService<CategoryService>();
            cityService = serviceProvider.GetRequiredService<CityService>();
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

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Entities.Article article = articleService.GetById(id);

            return View(article);
        }

        [HttpPost, ValidateAntiForgeryToken]
        // Only included second argument to distinguish between Get version.
        public IActionResult Delete(int id, IFormFile file)
        {
            Entities.Article article = articleService.GetById(id);
            bool result = articleService.Delete(article);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Something went wrong, please try again.";
                return View(article);
            }
        }


        public IActionResult Edit(int id)
        {
            Article article = articleService.GetById(id);
            List<Category> categories = categoryService.GetAll();
            List<City> cities = cityService.GetAll();

            var model = new UserViewModel
            {
                Article = article,
                CategoryList = categories,
                CityList = cities
            };


            return View(model);
        }



        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UserViewModel model)
        {
            Article article = model.Article;
            Article result = articleService.Update(article);
            if (result == null)
            {
                ViewBag.Error = "Something went wrong, please try again.";
                return View(article);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }


        public IActionResult Status(int id)
        {
            List<Article> articleList = articleService.GetStatus(id);
            ViewBag.Title = "Pending Articles";

            if (id==1)
            {
                ViewBag.Title = "Confirmed Articles";
            }
            else if (id==2)
            {
                ViewBag.Title = "Rejected Articles";
            }

            return View(articleList);
        }
    }
}
