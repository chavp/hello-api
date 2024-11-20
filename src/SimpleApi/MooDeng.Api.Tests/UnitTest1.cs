using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MooDeng.Api.Tests.Models;

namespace MooDeng.Api.Tests
{
    public class UnitTest1
    {
        protected readonly IConfigurationRoot _config = null;
        protected readonly DbContextOptionsBuilder<PartiesContext> _partiesBuilder = null;

        public UnitTest1()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _partiesBuilder = new DbContextOptionsBuilder<PartiesContext>()
                .UseSqlServer(_config.GetConnectionString("parties_db"));
        }
        [Fact]
        public void AddMooDeng()
        {
            using (var db = new PartiesContext(_partiesBuilder.Options))
            {
                var mooDeng = new Person("MooDeng");
                db.Add(mooDeng);
                db.SaveChanges();
            }
        }

        private static void CreateCommand(string queryString, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(
                       connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}