using DapperFantom.Entities;
using DapperFantom.Services;
using Microsoft.AspNetCore.Mvc;

namespace DapperFantom.Controllers
{
    public class CommentController : Controller
    {
        private CommentService commentService;
        private ArticleService articleService;

        public CommentController(IServiceProvider serviceProvider)
        {
            commentService = serviceProvider.GetRequiredService<CommentService>();
            articleService = serviceProvider.GetRequiredService<ArticleService>();
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add(string id, IFormCollection form)
        {
            Article article = articleService.GetByGuid(id);
            Comment comment = new Comment
            {
                ArticleId = article.ArticleId,
                CommentText = form["message"],
                Email = form["email"],
                Name = form["name"],
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now,
                Status = 1
            };

            int result = commentService.Add(comment);
            if (result > 0)
            {
                return Ok(new { success = "true" });
            }
            else
            {
                return Ok(new {success = "false"});
            }
        }
    }
}
