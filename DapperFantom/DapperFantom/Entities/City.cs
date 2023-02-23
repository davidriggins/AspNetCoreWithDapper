namespace DapperFantom.Entities
{
    public class City
    {
        [Dapper.Contrib.Extensions.Key]
        public int CityId { get; set; }
        public string CityName { get; set; }
    }
}
