using Adonet_Blog.Entities;
using Adonet_Blog.Models;
using Adonet_Blog.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Adonet_Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        PostService _postService;

        public HomeController(ILogger<HomeController> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _postService = new PostService(serviceProvider);
        }

        public IActionResult Index()
        {
            List<Post> myPostList = _postService.GetAll();
            BlogModel model = new BlogModel();
            model.postList = myPostList;

            return View(model);
        }


        public IActionResult PostDetail(int id)
        {
            Post myPost = _postService.Get(id);
            BlogModel model = new BlogModel()
            { 
                post = myPost
            };

            return View(model);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}