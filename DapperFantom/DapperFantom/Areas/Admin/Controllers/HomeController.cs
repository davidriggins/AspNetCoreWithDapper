using DapperFantom.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperFantom.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
