using DapperFantom.Entities;

namespace DapperFantom.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Category = new();
            Article = new();
            CategoryList = new List<Category>();
            ArticleList = new List<Article>();
            CityList = new List<City>();
            User = new Admin();
            UserList = new List<Admin>();
        }

        public Category Category { get; set; }
        public Article Article { get; set; }
        public List<Category> CategoryList { get; set; }
        public List<Article> ArticleList { get; set; }
        public List<City> CityList { get; set; }
        public Admin User { get; set; }
        public List<Admin> UserList { get; set; }
    }
}
