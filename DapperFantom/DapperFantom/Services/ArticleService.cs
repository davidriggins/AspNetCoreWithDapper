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
            catch (Exception ex)
            {

                throw;
            }

            return articles;
        }


        public int GetCount(int categoryId)
        {
            return Convert.ToInt32(connection.ExecuteScalar("select count(CategoryId) from Articles where CategoryId =" + categoryId).ToString());
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


        public List<Article> Search(string q)
        {
            List<Article> articles = new();
            try
            {
                articles = connection.Query<Article>(@"select * from Articles where Status=1 and Articles.Title like @q or Articles.Content like @q order by ArticleId DESC", new { q = "%" + q + "%" }).Take(5).ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return articles;
        }


        public int CountArticles()
        {
            return Convert.ToInt32(connection.ExecuteScalar("select count(ArticleId) from Articles where Status=1").ToString());
        }


        public List<Article> GetArticles(int page = 1)
        {
            List<Article> articles= new List<Article>();

            try
            {
                var parameters = new DynamicParameters();
                int pagesize = 3;
                int offset = (page - 1) * 3;

                parameters.Add("@offset", offset);
                parameters.Add("@pagesize", pagesize);

                string sql = @"select * from Articles
                               inner join Categorys as cat ON cat.CategoryId = Articles.CategoryId
                               where HomeView=1 and Status=1 or Slider=1
                               order by PublishingDate desc
                               OFFSET @offset ROWS
                               FETCH NEXT @pagesize ROWS ONLY";

                articles = connection.Query<Article, Category, Article>(sql, (article, category) =>
                {
                    article.Category = category;
                    return article;
                }, parameters, splitOn: "CategoryId").ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return articles;

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

                articles = connection.Query<Article, Category, Article>(sql, (article, category) =>
                {
                    article.Category = category;
                    return article;
                }, splitOn: "CategoryId").ToList();

            }
            catch (Exception)
            {

            }

            return articles;
        }


        public List<Article> GetByCategoryId(int id, int page = 1)
        {
            List<Article> articles = new List<Article>();

            try
            {
                var parameters = new DynamicParameters();
                int pageSize = 3;
                int offset = (page - 1) * 3;

                parameters.Add("@id", id);
                parameters.Add("@offset", offset);
                parameters.Add("@pagesize", pageSize);

                string sql = @"select * from Articles where Status=1 and CategoryId = @id
                               order by PublishingDate desc
                               OFFSET @offset ROWS
                               FETCH NEXT @pagesize ROWS ONLY";

                articles = connection.Query<Article>(sql, parameters).ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return articles;
        }


        public Article GetPrev(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@id", id);

            string sql = @"select ArticleId,Guid,Title,Image from Articles 
                          where ArticleId<@id order by ArticleId desc 
                          OFFSET 0 ROWS
                          FETCH NEXT 1 ROWS ONLY";

            return connection.Query<Article>(sql, parameter).FirstOrDefault();
        }

        public Article GetNext(int id)
        {
            var parameter = new DynamicParameters();
            parameter.Add("@id", id);

            string sql = @"select top 1 ArticleId,Guid,Title,Image from Articles 
                          where ArticleId>@id order by ArticleId asc";

            return connection.Query<Article>(sql, parameter).FirstOrDefault();
        }
    }
}
