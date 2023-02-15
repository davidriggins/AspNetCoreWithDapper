using Adonet_Blog.Entities;
using Adonet_Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Adonet_Blog.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login(User user)
        {
            BlogModel model = new BlogModel()
            {
                user = new Entities.User(),
            };

            return View();
        }
    }
}
