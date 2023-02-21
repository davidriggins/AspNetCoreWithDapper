using Dapper;
using DapperFantom.Entities;
using System.Data;
using System.Data.SqlClient;

namespace DapperFantom.Services
{
    public class CategoryService
    {
        private SqlConnection adoNetConnection;
        private IDbConnection dapperConnection;
        private ConnectionService connectionService;

        public CategoryService(IConfiguration configuration)
        {
            connectionService= new ConnectionService(configuration);
            adoNetConnection = connectionService.DbConnection();
            dapperConnection = connectionService.ForDapper();
        }

        public List<Category> GetAllAdoNet()
        {
            List<Category> categories = new List<Category>();
            SqlCommand command = new SqlCommand("select * from Category", adoNetConnection);
            command.CommandType = CommandType.Text;
            IDataReader dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            while (dataReader.Read())
            {
                Category category = new Category();
                category.CategoryId = dataReader["CategoryId"] is DBNull ? 0 : int.Parse(dataReader["CategoryId"].ToString());
                category.CategoryName = dataReader["CategoryName"] is DBNull ? string.Empty : dataReader["CategoryName"].ToString();
                category.Slug = dataReader["Slug"] is DBNull ? string.Empty : dataReader["Slug"].ToString();
                category.OrderBy = dataReader["OrderBy"] is DBNull ? 0 : int.Parse(dataReader["OrderBy"].ToString());
                categories.Add(category);

            }

            return categories;
        }

        public List<Category> GetAllDapper()
        {
            List<Category> categories = new List<Category>();
            try
            {
                categories = dapperConnection.Query<Category>(@"select * from Category").ToList();
            }
            catch (Exception ex)
            {

            }

            return categories;
        }

    }
}
