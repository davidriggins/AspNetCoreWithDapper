using DapperFantom.Models;
using DapperFantom.Services;
using Microsoft.AspNetCore.Mvc;

namespace DapperFantom.Areas.Admin.Controllers
{
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
    }
}
