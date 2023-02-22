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
    }
}
