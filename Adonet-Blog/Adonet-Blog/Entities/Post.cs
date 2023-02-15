namespace Adonet_Blog.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Publishing_Date { get; set; }
        public DateTime Modified_Date { get; set; }
        public User Writer { get; set; }
    }
}
