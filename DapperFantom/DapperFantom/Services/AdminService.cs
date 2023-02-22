using Dapper;
using DapperFantom.Entities;
using System.Data;

namespace DapperFantom.Services
{
    public class AdminService
    {
        private IDbConnection connection;
        private ConnectionService connectionService;

        public AdminService(IConfiguration configuration)
        {
            connectionService = new ConnectionService(configuration);
            connection = connectionService.ForDapper();
        }


        public Admin Login(Admin admin)
        {
            Admin myAdmin = new();

            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@username", admin.Username);
                parameters.Add("@password", admin.Password);

                myAdmin = connection.Query<Admin>($@"select AdminId,Username,Password from Admins where Username=@username and Password=@password", parameters).FirstOrDefault();
            }
            catch (Exception ex)
            {

            }

            return myAdmin;
        }
    }
}
