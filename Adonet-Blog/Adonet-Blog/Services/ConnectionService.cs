using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

namespace Adonet_Blog.Services
{
    public class ConnectionService
    {
        private SqlConnection myConnection;
        private IConfiguration _configuration;

        public ConnectionService(IConfiguration configuration)
        {
            _configuration = configuration;
            string connectionString = configuration.GetConnectionString("DefaultConnection").ToString();

            myConnection = new SqlConnection(connectionString);

            if (myConnection.State != ConnectionState.Open)
            {
                myConnection.Open();
            }
        }

        internal SqlConnection DbConnection()
        {
            return myConnection;
        }
    }
}
