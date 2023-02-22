using DapperFantom.Services;
using Microsoft.AspNetCore.Mvc;

namespace DapperFantom.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private AdminService adminService;

        public LoginController(IServiceProvider serviceProvider)
        {
            adminService = serviceProvider.GetRequiredService<AdminService>();
        }


        public IActionResult Index()
        {
            Entities.Admin admin = new();


            return View(admin);
        }
    }
}
