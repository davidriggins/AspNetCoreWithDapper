using Adonet_Blog.Entities;
using Adonet_Blog.Models;
using Adonet_Blog.Services;
using Microsoft.AspNetCore.Mvc;

namespace Adonet_Blog.Controllers
{

    public class UserController : Controller
    {
        private UserService userService;
        private PostService postService;

        public UserController(IConfiguration configuration)
        {
            userService = new UserService(configuration);
            postService= new PostService(configuration);
        }



        public IActionResult Index()
        {
            string cookieUsername = HttpContext.Request.Cookies["username"];
            string cookieUserId = HttpContext.Request.Cookies["userid"];

            if (cookieUserId == "" || cookieUserId == null ||
                cookieUsername == "" || cookieUsername == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                List<Post> posts = postService.GetAll();
                BlogModel model = new BlogModel()
                {
                    postList = posts
                };

                return View(model);
            }

        }


        [HttpGet]
        public IActionResult Login()
        {
            LoginModel model = new()
            {
                user = new Entities.User(),
                Success = true
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult Login(User formUser)
        {
            LoginModel model = new()
            {
                user = new Entities.User(),
            };

            User myUser = userService.Login(formUser);
            if (myUser.UserId > 0 && myUser.UserName != "" && myUser.Password != "")
            {
                CookieOptions userid = new CookieOptions();
                userid.Expires = DateTime.Now.AddDays(5);
                Response.Cookies.Append("userid", myUser.UserId.ToString(), userid);

                CookieOptions username = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(5),
                };
                Response.Cookies.Append("username", myUser.UserName, username);

                return RedirectToAction("Index");
            }
            else
            {
                model = new LoginModel()
                {
                    Success = false,
                    user = new User(),
                    Message = "Please check your UserName and Password."
                };

                return View("Login", model);
            }

        }
    }
}
