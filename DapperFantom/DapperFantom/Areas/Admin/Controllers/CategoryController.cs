using DapperFantom.Areas.Admin.Models;
using DapperFantom.Models;
using DapperFantom.Services;
using Microsoft.AspNetCore.Mvc;

namespace DapperFantom.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    public class CategoryController : Controller
    {
        private CategoryService categoryService;
        public CategoryController(IServiceProvider serviceProvider)
        {
            categoryService = serviceProvider.GetRequiredService<CategoryService>();
        }


        public IActionResult Index()
        {
            List<Entities.Category> categoryList = categoryService.GetAll();

            var model = new UserViewModel
            {
                CategoryList = categoryList,
            };

            return View(model);
        }


        [HttpGet]
        public IActionResult Add()
        {
            Entities.Category category = new Entities.Category();
            return View(category);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Add(Entities.Category category)
        {
            int result = categoryService.Add(category);
            if (result == 0)
            {
                ViewBag.Error = "Something went wrong, please try again.";
                return View(category);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Entities.Category category = categoryService.Get(id);

            return View(category);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Entities.Category category)
        {
            bool result = categoryService.Update(category);
            if (!result)
            {
                ViewBag.Error = "Something went wrong, please try again.";
                return View(category);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            Entities.Category category = categoryService.Get(id);

            return View(category);
        }

        [HttpPost, ValidateAntiForgeryToken]
        // Only included second argument to distinguish between Get version.
        public IActionResult Delete(int id, IFormFile file)
        {
            Entities.Category category = categoryService.Get(id);
            bool result = categoryService.Delete(category);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Something went wrong, please try again.";
                return View(category);
            }
        }
    }
}
