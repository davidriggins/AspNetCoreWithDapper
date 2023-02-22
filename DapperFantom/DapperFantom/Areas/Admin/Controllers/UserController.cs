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


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Entities.Admin admin = adminService.Get(id);

            return View(admin);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Entities.Admin admin)
        {
            Entities.Admin result = adminService.Update(admin);
            if (result == null)
            {
                ViewBag.Error = "Something went wrong, please try again.";
                return View(admin);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            Entities.Admin admin = adminService.Get(id);

            return View(admin);
        }

        [HttpPost,ValidateAntiForgeryToken]
        // Only included second argument to distinguish between Get version.
        public IActionResult Delete(int id, IFormFile file)
        {
            Entities.Admin admin = adminService.Get(id);
            bool result = adminService.Delete(admin);
            if (result)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Something went wrong, please try again.";
                return View(admin);
            }
        }
    }
}
