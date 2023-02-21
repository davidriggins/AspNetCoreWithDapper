using Microsoft.AspNetCore.Mvc;

namespace DapperFantom.Controllers
{
    public class ArticleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Add()
        {
            return View();
        }
    }
}
