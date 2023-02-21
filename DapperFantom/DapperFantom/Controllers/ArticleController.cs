using DapperFantom.Entities;
using DapperFantom.Models;
using DapperFantom.Services;
using Microsoft.AspNetCore.Mvc;

namespace DapperFantom.Controllers
{

    public class ArticleController : Controller
    {
        private CategoryService categoryService;
        private CityService cityService;

        public ArticleController(IServiceProvider serviceProvider)
        {
            categoryService = serviceProvider.GetRequiredService<CategoryService>();
            cityService = serviceProvider.GetRequiredService<CityService>();
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
        public async Task<IActionResult> Save(Article model, IFormFile file) 
        {
            if (ModelState.IsValid)
            {
                Guid guid = Guid.NewGuid();
                model.Guid = guid.ToString();
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.PublishingDate= DateTime.Now;
            }
            else
            {
                var message = string.Join("|", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
            }

            return View();
        }
    }
}
