using DapperFantom.Entities;

namespace DapperFantom.Models
{
    public class GeneralViewModel
    {
        public GeneralViewModel()
        {
            Category = new();
            Article= new();
            CategoryList = new List<Category>();
            ArticleList = new List<Article>();
            CityList= new List<City>();
            Comments = new List<Comment>();
        }

        public Category Category { get; set; }
        public Article Article { get; set; }
        public List<Category> CategoryList { get; set; }
        public List<Article> ArticleList { get; set; }
        public List<City> CityList { get; set; }
        public Article PrevArticle { get; set; }
        public Article NextArticle { get; set; }
        public PaginationModel PaginationModel { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
