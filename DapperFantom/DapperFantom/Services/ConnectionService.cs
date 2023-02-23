using System.Data;
using System.Data.SqlClient;

namespace DapperFantom.Services
{
    public class ConnectionService
    {
        private SqlConnection myconnection;
        private IConfiguration _configuration;

        public ConnectionService(IConfiguration configuration)
        {
            _configuration = configuration;
            string connectionString = _configuration.GetConnectionString("DefaultConnection").ToString();
            myconnection = new SqlConnection(connectionString);
            //if (myconnection.State != ConnectionState.Open)
            //{
            //    myconnection.Open();
            //}
        }

        internal SqlConnection DbConnection()
        {
            return myconnection;
        }

        internal SqlConnection ForDapper()
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection").ToString());
            //var state = connection.State;
            //if (state != ConnectionState.Open)
            //{
            //    connection.Open();
            //}

            return connection;
        }
    }
}
