using Adonet_Blog.Entities;
using System.Data;
using System.Data.SqlClient;

namespace Adonet_Blog.Services
{
    public class PostService
    {
        private SqlConnection myConnection;
        SqlCommand myCommand;

        public PostService(IConfiguration configuration)
        {
            ConnectionService connectionService = new ConnectionService(configuration);
            myConnection = connectionService.DbConnection();
        }

        public List<Post> GetAll()
        {
            List<Post> posts = new List<Post>();

            myCommand = new SqlCommand("select * from Post", myConnection);
            myCommand.CommandType = CommandType.Text;
            IDataReader dataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                Post post = new();
                post.PostId = dataReader["PostId"] is DBNull ? 0 : int.Parse(dataReader["PostId"].ToString());
                post.UserId = dataReader["UserId"] is DBNull ? 0 : int.Parse(dataReader["UserId"].ToString());
                post.Title = dataReader["Title"] is DBNull ? string.Empty : dataReader["Title"].ToString();
                post.Content = dataReader["Content"] is DBNull ? string.Empty : dataReader["Content"].ToString();

                if (dataReader["Publishing_Date"] != DBNull.Value)
                {
                    post.Publishing_Date = DateTime.Parse(dataReader["Publishing_Date"].ToString());
                }

                if (dataReader["Modified_Date"] != DBNull.Value)
                {
                    post.Publishing_Date = DateTime.Parse(dataReader["Modified_Date"].ToString());
                }

                posts.Add(post);
            }

            return posts;
        }


        public Post Get(int id)
        {
            Post post = new Post();

            string mySqlQuery = "select Post.*, [User].Username from Post right join [User] ON [User].UserId = Post.UserId where Post.PostId = @id";
            //string mySqlQuery = "select * from Post where PostId = @id";

            myCommand = new SqlCommand(mySqlQuery, myConnection);
            myCommand.CommandType = CommandType.Text;
            myCommand.Parameters.AddWithValue("@id", id);
            IDataReader dataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);

            while (dataReader.Read())
            {
                post.PostId = dataReader["PostId"] is DBNull ? 0 : int.Parse(dataReader["PostId"].ToString());
                post.UserId = dataReader["UserId"] is DBNull ? 0 : int.Parse(dataReader["UserId"].ToString());
                post.Title = dataReader["Title"] is DBNull ? string.Empty : dataReader["Title"].ToString();
                post.Content = dataReader["Content"] is DBNull ? string.Empty : dataReader["Content"].ToString();

                if (dataReader["Publishing_Date"] != DBNull.Value)
                {
                    post.Publishing_Date = DateTime.Parse(dataReader["Publishing_Date"].ToString());
                }

                if (dataReader["Modified_Date"] != DBNull.Value)
                {
                    post.Publishing_Date = DateTime.Parse(dataReader["Modified_Date"].ToString());
                }

                User myUser = new User()
                {
                    UserId = post.UserId,
                    UserName = dataReader["Username"] is DBNull ? "This post has been deleted!" : dataReader["Username"].ToString()
                };

                post.Writer = myUser;
            }

            return post;
        }

    }
}
