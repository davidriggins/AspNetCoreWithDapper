using DapperFantom.Entities;

namespace DapperFantom.Models
{
    public class GeneralViewModel
    {
        public Category Category { get; set; }
        public Article Article { get; set; }
        public List<Category> CategoryList { get; set; }
        public List<Article> ArticleList { get; set; }
    }
}
