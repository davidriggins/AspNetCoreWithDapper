using Microsoft.AspNetCore.Authentication;

namespace DapperFantom.Entities
{
    public class Article
    {
        public int ArticleId { get; set; }
        public string Guid { get; set; }
        public int CategoryId { get; set; }
        public string NameSurname { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public int CityId { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
        public string Content { get; set; }
        public DateTime PublishingDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Seen { get; set; }
        public int Status { get; set; }
        public int HomeView { get; set; }
        public int Hit { get; set; }
        public int CommentCount { get; set; }

    }
}
