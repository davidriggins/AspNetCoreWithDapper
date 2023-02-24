using DapperFantom.Entities;

namespace DapperFantom.Models
{
    public class PaginationModel
    {
        public int TotalCount;
        public List<Article> ArticleList;
        public string Html;
        public int PageCount;
    }
}
