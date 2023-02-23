using Dapper;
using Dapper.Contrib.Extensions;
using DapperFantom.Entities;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;

namespace DapperFantom.Services
{
    public class CategoryService
    {
        private SqlConnection adoNetConnection;
        private IDbConnection connection;
        private ConnectionService connectionService;

        public CategoryService(IConfiguration configuration)
        {
            connectionService= new ConnectionService(configuration);
            adoNetConnection = connectionService.DbConnection();
            connection = connectionService.ForDapper();
        }

        public List<Category> GetAllAdoNet()
        {
            List<Category> categories = new List<Category>();
            SqlCommand command = new SqlCommand("select * from Categorys", adoNetConnection);
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


        public Category GetBySlug(string slug)
        {
            Category category = new Category();
            var parameters = new DynamicParameters();
            parameters.Add("@slug", slug);

            category = connection.Query<Category>("select * from Categorys whre Slug=@slug", parameters).FirstOrDefault();
            return category;
        }


        public List<Category> GetAll()
        {
            List<Category> categories = new List<Category>();
            try
            {
                categories = connection.Query<Category>(@"select * from Categorys").ToList();
            }
            catch (Exception ex)
            {

            }

            return categories;
        }

        public int Add(Category category)
        {
            //long result = connection.Insert(category);

            try
            {
                long result = connection.Insert(category);
                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                return 0;
            }

        }


        public Category Get(int id)
        {
            Category category = new Category();
            var parameters = new DynamicParameters();
            parameters.Add("@id", id);
            category = connection.Query<Category>("select * from Categorys where CategoryId=@id", parameters).FirstOrDefault();
            return category;
        }

        //public Category Update(Category category)
        //{
        //    try
        //    {
        //        var parameters = new DynamicParameters();
        //        parameters.Add("@id", category.CategoryId);
        //        parameters.Add("@category", category.CategoryName);
        //        parameters.Add("@slug", category.Slug);

        //        connection.Execute("update Categorys set CategoryName=@category, Slug=@slug where CategoryId=@id", parameters);
        //        return category;
        //    }
        //    catch (Exception)
        //    {
        //        return new Category();
        //    }
        //}

        public bool Update(Category category)
        {
            try
            {
                bool result = connection.Update(category);
                return result;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public bool Delete(Category category)
        {
            try
            {
                bool result = connection.Delete(category);
                return result;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
