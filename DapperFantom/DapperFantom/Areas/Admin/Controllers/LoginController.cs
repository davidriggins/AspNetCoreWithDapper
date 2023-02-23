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

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Index(Entities.Admin admin)
        {
            if (admin.Username != "" || admin.Password != "")
            {
                Entities.Admin myAdmin = adminService.Login(admin);
                if (myAdmin == null)
                {
                    ViewBag.Error = "Something went wrong, please try again!";
                    return View(admin);
                }
                else
                {
                    CookieOptions useridCookie = new CookieOptions();
                    useridCookie.Expires= DateTime.Now.AddDays(3);
                    Response.Cookies.Append("userid", myAdmin.AdminId.ToString(), useridCookie);

                    CookieOptions usernameCookie = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(3),
                    };
                    Response.Cookies.Append("username", myAdmin.Username, usernameCookie);

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.Error = "Please check your username or password";
                return View(admin);
            }

        }


        public IActionResult Logout()
        {
            if (HttpContext.Request.Cookies.Count > 0)
            {
                foreach (var item in HttpContext.Request.Cookies.Keys) 
                { 
                    Response.Cookies.Delete(item);
                }
            }

            return Redirect("/");
        }
    }
}
