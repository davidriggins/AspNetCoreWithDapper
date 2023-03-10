using DapperFantom.Entities;
using DapperFantom.Helpers;
using DapperFantom.Models;
using DapperFantom.Services;
using Microsoft.AspNetCore.Mvc;

namespace DapperFantom.Controllers
{

    public class ArticleController : Controller
    {
        private CategoryService categoryService;
        private CityService cityService;
        private IWebHostEnvironment hosting;
        private ArticleService articleService;
        private CommentService commentService;

        public ArticleController(IServiceProvider serviceProvider)
        {
            categoryService = serviceProvider.GetRequiredService<CategoryService>();
            cityService = serviceProvider.GetRequiredService<CityService>();
            hosting = serviceProvider.GetRequiredService<IWebHostEnvironment>();
            articleService = serviceProvider.GetRequiredService<ArticleService>();
            commentService = serviceProvider.GetRequiredService<CommentService>();
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Add()
        {
            List<Category> categories = categoryService.GetAll();
            List<City> cities = cityService.GetAll();

            GeneralViewModel model = new GeneralViewModel
            {
                CategoryList = categories,
                CityList = cities,
                Article = new()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(Article model, IFormFile file) 
        {
            if (ModelState.IsValid)
            {
                Guid guid = Guid.NewGuid();
                model.Guid = guid.ToString();
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.PublishingDate= DateTime.Now;

                if (file.Length> 0)
                {
                    UploadHelpers uploadHelpers = new UploadHelpers(hosting);
                    string fileName = await uploadHelpers.Upload(file);
                    if (fileName != "")
                    {
                        model.Image = fileName;
                    }
                }

                int result = articleService.Add(model);
                if (result > 0)
                {
                    Article article = articleService.GetById(result);

                    return RedirectToAction("Detail", new { id = article.Guid });
                }
                else
                {
                    string message = "Something went wrong, please check it.";
                    return View(message);
                }

            }
            else
            {
                var message = string.Join("|", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
            }

            return View();
        }


        //[Route("/View/{guid}")]
        public IActionResult Detail(string id)
        {
            //Article article = articleService.GetById(id);
            Article article = articleService.GetByGuid(id);
            Article prevArt = articleService.GetPrev(article.ArticleId);
            Article nextArt = articleService.GetNext(article.ArticleId);
            List<Comment> comments = commentService.GetByArticle(article.ArticleId);

            GeneralViewModel model = new GeneralViewModel
            {
                Article = article,
                PrevArticle = prevArt, 
                NextArticle = nextArt,
                Comments = comments
            };

            return View(model);
        }
    }
}
