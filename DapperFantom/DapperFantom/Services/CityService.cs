using Dapper;
using DapperFantom.Entities;
using System.Data;

namespace DapperFantom.Services
{
    public class CityService
    {
        private IDbConnection dapperConnection;
        private ConnectionService connectionService;

        public CityService(IConfiguration configuration)
        {
            connectionService = new ConnectionService(configuration);
            dapperConnection = connectionService.ForDapper();
        }


        public List<City> GetAll()
        {
            List<City> cities = new List<City>();
            try
            {
                cities = dapperConnection.Query<City>(@"select * from City").ToList();
            }
            catch (Exception ex)
            {

            }

            return cities;
        }
    }
}
