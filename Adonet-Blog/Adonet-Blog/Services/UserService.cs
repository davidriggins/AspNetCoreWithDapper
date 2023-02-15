using Adonet_Blog.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Adonet_Blog.Services
{
    public class UserService
    {
        private SqlConnection myConnection;
        SqlCommand myCommand;

        public UserService(IConfiguration configuration)
        {
            ConnectionService connectionService = new ConnectionService(configuration);
            myConnection = connectionService.DbConnection();
        }


        public User Get(int id)
        {
            User myUser = new();
            string mySqlQuery = "select * from [User] where UserId = @id";
            myCommand = new SqlCommand();
            myCommand.CommandText = mySqlQuery;
            myCommand.Connection = myConnection;
            myCommand.CommandType = CommandType.Text;
            myCommand.Parameters.AddWithValue("@id", id);
            IDataReader dataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                myUser.UserId = dataReader["UserId"] is DBNull ? 0 : int.Parse(dataReader["UserId"].ToString());
                myUser.UserName = dataReader["Username"] is DBNull ? string.Empty : dataReader["Username"].ToString();
                myUser.Password = dataReader["Password"] is DBNull ? string.Empty : dataReader["Password"].ToString();

            }

            return myUser;
        }


        public List<User> GetAll()
        {
            List<User> users = new List<User>();

            string mySqlQuery = "select * from [User]";
            myCommand = new SqlCommand(mySqlQuery, myConnection);
            myCommand.CommandType = CommandType.Text;
            IDataReader dataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                User myUser = new();
                myUser.UserId = dataReader["UserId"] is DBNull ? 0 : int.Parse(dataReader["UserId"].ToString());
                myUser.UserName = dataReader["Username"] is DBNull ? string.Empty : dataReader["Username"].ToString();
                myUser.Password = dataReader["Password"] is DBNull ? string.Empty : dataReader["Password"].ToString();

                users.Add(myUser);
            }

            return users;
        }


        public User Login(User user)
        {
            return Login(user.UserName, user.Password);
        }

        public User Login(string username, string password)
        {
            User myUser = new User();
            string mySqlQuery = "select * from [User] where Username = @usernmae and Password = @password";
            myCommand = new SqlCommand(mySqlQuery, myConnection);
            myCommand.Parameters.AddWithValue("@username", username);
            myCommand.Parameters.AddWithValue("@password", password);
            IDataReader dataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                myUser.UserId = dataReader["UserId"] is DBNull ? 0 : int.Parse(dataReader["UserId"].ToString());
                myUser.UserName = dataReader["Username"] is DBNull ? string.Empty : dataReader["Username"].ToString();
                myUser.Password = dataReader["Password"] is DBNull ? string.Empty : dataReader["Password"].ToString();
            }

            return myUser;
        }
    }
}
