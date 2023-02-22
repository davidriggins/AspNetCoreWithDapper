using Dapper;
using Dapper.Contrib.Extensions;
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


        public List<Admin> GetAll()
        {
            List<Admin> userList = new List<Admin>();

            try
            {
                userList = connection.Query<Admin>("select * from Admins").ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return userList;
        }


        public int Add(Admin admin)
        {
            long result = connection.Insert(admin);
            return Convert.ToInt32(result);
        }


        public Admin Get(int id)
        {
            Admin myAdmin = new Admin();
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            myAdmin = connection.Query<Admin>("select * from Admins where AdminId=@id", parameters).FirstOrDefault();
            return myAdmin;
        }

        public Admin Update(Admin admin)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", admin.AdminId);
                parameters.Add("@username", admin.Username);
                parameters.Add("@password", admin.Password);

                connection.Execute("update Admins set Username=@username, Password=@password where AdminId=@id", parameters);
                return admin;
            }
            catch (Exception)
            {
                return new Admin();
            }
        }

        public bool Delete(Admin admin)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id", admin.AdminId);

                connection.Execute("delete from Admins where AdminId=@id", parameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
