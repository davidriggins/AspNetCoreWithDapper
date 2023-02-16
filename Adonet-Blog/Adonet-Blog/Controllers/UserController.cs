using Adonet_Blog.Entities;
using Adonet_Blog.Models;
using Adonet_Blog.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Adonet_Blog.Controllers
{

    public class UserController : Controller
    {
        private UserService userService;
        private PostService postService;

        public UserController(IConfiguration configuration)
        {
            userService = new UserService(configuration);
            postService = new PostService(configuration);
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


        [HttpGet]
        public IActionResult Create()
        {
            BlogModel model = new BlogModel();

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            post.Publishing_Date = DateTime.Now;
            post.Modified_Date = DateTime.Now;

            post.UserId = int.Parse(HttpContext.Request.Cookies["userid"].ToString());
            bool success = postService.Create(post);

            if (success)
            {
                return RedirectToAction("Index");
            }

            else
            {
                ViewBag.Error = "Something went wrong creating the post.";
                return View();
            }
        }


        [HttpGet]
        public IActionResult Update(int id)
        {
            Post myPost = postService.Get(id);

            if (myPost.PostId == 0)
            {
                return RedirectToAction("Index");
            }

            BlogModel model = new BlogModel()
            {
                post = myPost
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult Update(Post post)
        {
            post.Modified_Date = DateTime.Now;
            Boolean success = postService.Update(post);

            if (success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Something went wrong updating the post.";
                return View();
            }
        }
    }
}
