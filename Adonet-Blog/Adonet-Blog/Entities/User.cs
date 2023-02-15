namespace Adonet_Blog.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<Post> Post { get; set; }
    }
}
