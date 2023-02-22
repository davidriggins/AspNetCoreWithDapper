using DapperFantom.Areas.Admin.Models;
using DapperFantom.Models;
using DapperFantom.Services;
using Microsoft.AspNetCore.Mvc;

namespace DapperFantom.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuth]
    public class UserController : Controller
    {
        private AdminService adminService;

        public UserController(IServiceProvider serviceProvider)
        {
            adminService = serviceProvider.GetRequiredService<AdminService>();
        }

        public IActionResult Index()
        {
            List<Entities.Admin> userList = adminService.GetAll();

            var model = new UserViewModel
            {
                UserList = userList,
            };

            return View(model);
        }


        [HttpGet]
        public IActionResult Add() 
        { 
            Entities.Admin admin = new Entities.Admin();
            return View(admin);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Add(Entities.Admin admin)
        {
            int result = adminService.Add(admin);
            if (result == 0)
            {
                ViewBag.Error = "Something went wrong, please try again.";
                return View(admin);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
