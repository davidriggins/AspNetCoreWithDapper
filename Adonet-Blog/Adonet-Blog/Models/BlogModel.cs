using Adonet_Blog.Entities;

namespace Adonet_Blog.Models
{
    public class BlogModel
    {
        public List<Post> postList { get; set; }
        public Post post { get; set; }
        public List<User> userList { get; set; }
    }
}
