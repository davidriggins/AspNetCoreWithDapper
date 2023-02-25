using Dapper.Contrib.Extensions;
using DapperFantom.Entities;
using System.Data;

namespace DapperFantom.Services
{
    public class CommentService
    {

        private IDbConnection connection;
        private ConnectionService connectionService;

        public CommentService(IConfiguration configuration)
        {
            connectionService= new ConnectionService(configuration);
            connection = connectionService.ForDapper();
        }

        public int Add(Comment comment)
        {
            try
            {
                long result = connection.Insert(comment);
                return Convert.ToInt32(result);
            }
            catch (Exception)
            {

                return 0;
            }
        }
    }
}
