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


        public IActionResult Add()
        {
            List<Category> categories = categoryService.GetAll();
            List<City> cities = cityService.GetAll();

            GeneralViewModel model = new GeneralViewModel
            {
                CategoryList = categories,
                CityList = cities
            };

            return View(model);
        }
    }
}
