using Dapper;
using Dapper.Contrib.Extensions;
using DapperFantom.Entities;
using System.Data;

namespace DapperFantom.Services
{
    public class ArticleService
    {
        private IDbConnection connection;
        private ConnectionService connectionService;

        public ArticleService(IConfiguration configuration)
        {
            connectionService = new ConnectionService(configuration);
            connection = connectionService.ForDapper();
        }


        public List<Article> GetStatus(int status)
        {
            List<Article> articles = new List<Article>();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@status", status);
                articles = connection.Query<Article>(@"select * from Articles where Status=@status", parameters).ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return articles;
        }

        public int Add(Article article)
        {
            var result = connection.Insert(article);
            if (result > 0)
            {
                return int.Parse(result.ToString());
            }
            else
            {
                return 0;
            }
        }

        public Article GetById(int id)
        {
            Article article = new();

            try
            {
                article = connection.Query<Article>(@"select * from Articles where ArticleId = " + id).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }

            return article;
        }


        public Article GetByGuid(string guid)
        {
            Article article = new();

            try
            {

                var parameter = new DynamicParameters();
                parameter.Add("@guid", guid);
                article = connection.Query<Article>(@"select * from Articles where Guid = @guid", parameter).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }

            return article;
        }

        public List<Article> GetAll()
        {
            List<Article> articles = new List<Article>();

            try
            {
                articles = connection.Query<Article>(@"select * from Articles").ToList();
            }
            catch (Exception)
            {

            }

            return articles;
        }


        public bool Delete(Article article)
        {
            try
            {
                bool result = connection.Delete(article);
                return result;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public Article Update(Article article)
        {
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", article.ArticleId);
                parameters.Add("@title", article.Title);
                parameters.Add("@category", article.CategoryId);
                parameters.Add("@city", article.CityId);
                parameters.Add("@status", article.Status);
                parameters.Add("@homeview", article.HomeView);
                parameters.Add("@slider", article.Slider);
                parameters.Add("@seen", 1);
                parameters.Add("@publishingDate", DateTime.Now);

                try
                {
                    connection.Execute(@"update Articles set Title=@title,CategoryId=@category,CityId=@city,Status=@status,
                                        HomeView=@homeview,Slider=@slider,Seen=@seen,PublishingDate=@publishingDate 
                                        where ArticleId=@id", parameters);

                    return article;
                }
                catch (Exception ex)
                {

                    return new Article();
                }
            }
        }


        public List<Article> GetHome()
        {
            List<Article> articles = new List<Article>();

            try
            {
                //articles = connection.Query<Article>("select * from Articles where HomeView=1 and Status=1 or Slider=1").ToList();
                string sql = @"select * from Articles
                                    inner join Categorys as cat on cat.CategoryId = Articles.CategoryId
                               where HomeView=1 and Status=1 or Slider=1";

                articles = connection.Query<Article,Category,Article>(sql, (article, category) =>
                {
                    article.Category = category;
                    return article;
                }, splitOn:"CategoryId").ToList();

            }
            catch (Exception)
            {

            }

            return articles;
        }

        
        public List<Article> GetByCategoryId(int id)
        {
            List<Article> articles = new List<Article>();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", id);

                string sql = @"select * from Articles where Status=1 and CategoryId = @id";

                articles = connection.Query<Article>(sql, parameters).ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return articles;
        }
    }
}
