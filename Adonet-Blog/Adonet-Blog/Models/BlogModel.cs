using Adonet_Blog.Entities;

namespace Adonet_Blog.Models
{
    public class BlogModel
    {
        public BlogModel()
        {
            postList = new List<Post>();
            post = new Post();
            userList= new List<User>();
            user = new User();
        }

        public List<Post> postList { get; set; }
        public Post post { get; set; }
        public List<User> userList { get; set; }
        public User user { get; set; }
    }
}
