using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MooDeng.Parties.Mappings;
using MooDeng.Parties.Models;
using MooDeng.Parties.Services;

namespace MooDeng.Api.Tests
{
    public class UnitTest1
    {
        protected readonly IConfigurationRoot _config = null;
        protected readonly DbContextOptionsBuilder<PartiesContext> _partiesBuilder = null;
        protected readonly TestDbContextFactory _testDbContextFactory;

        public UnitTest1()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _partiesBuilder = new DbContextOptionsBuilder<PartiesContext>()
                .UseSqlServer(_config.GetConnectionString("parties_db"));

            _testDbContextFactory = new TestDbContextFactory(_config.GetConnectionString("parties_db"));
        }

        [Fact]
        public void AddRolePet()
        {
            using (var db = _testDbContextFactory.CreateDbContext())
            {
                var pet = new PartyRoleType(PartyRoleType.Pet);
                db.Add(pet);

                var zoo = new PartyRoleType(PartyRoleType.Zoo);
                db.Add(zoo);

                var bringUp = new RelationshipPartyType(RelationshipPartyType.BringUp);
                db.Add(bringUp);

                db.SaveChanges();
            }
        }

        [Fact]
        public void AddMooDeng()
        {
            using (var db = _testDbContextFactory.CreateDbContext())
            {
                var mooDeng = new Animal{ Name = "MooDeng" };
                db.Add(mooDeng);
                var petRoleType = db.PartyRoleTypes.Single(x => x.Code == PartyRoleType.Pet);
                var mooDengPet = new PartyRole(petRoleType, mooDeng);
                db.Add(mooDengPet);

                var zoo = new Organization("KKOZ")
                {
                    Name = "Khao Kheow Open Zoo"
                };
                db.Add(zoo);
                var zooRoleType = db.PartyRoleTypes.Single(x => x.Code == PartyRoleType.Zoo);
                var mooDengZoo = new PartyRole(zooRoleType, zoo);
                db.Add(mooDengZoo);

                var bringUpType = db.RelationshipPartyTypes.Single(x => x.Code == RelationshipPartyType.BringUp);
                var re = new RelationshipParty(bringUpType, mooDengZoo, mooDengPet);
                db.Add(re);
                //db.RemoveRange(db.RelationshipPartyRoles);
                //db.RemoveRange(db.PartyRoles);
                //db.RemoveRange(db.Parties);

                db.SaveChanges();
            }
        }

        [Fact]
        public void AddMooTun()
        {
            using (var db = _testDbContextFactory.CreateDbContext())
            {
                var mooTun = new Animal { Name = "MooTun" };
                db.Add(mooTun);
                var petRoleType = db.PartyRoleTypes.Single(x => x.Code == PartyRoleType.Pet);
                var mooTunPet = new PartyRole(petRoleType, mooTun);
                db.Add(mooTunPet);

                var zoo = db.Parties.OfType<Organization>().Single(x => x.Code == "KKOZ");
                var zooRoleType = db.PartyRoleTypes.Single(x => x.Code == PartyRoleType.Zoo);

                var roleZoo = db.PartyRoles.Single(x => x.Party == zoo && x.PartyRoleType == zooRoleType);

                var bringUpType = db.RelationshipPartyTypes.Single(x => x.Code == RelationshipPartyType.BringUp);
                var re = new RelationshipParty(bringUpType, roleZoo, mooTunPet);
                db.Add(re);
                //db.RemoveRange(db.RelationshipPartyRoles);
                //db.RemoveRange(db.PartyRoles);
                //db.RemoveRange(db.Parties);

                db.SaveChanges();
            }
        }

        [Fact]
        public async Task GetPetsFromZoo()
        {
            var service = new PartiesService(_testDbContextFactory);
            var zoos = await service.GetOrganizationByRoleTypeCodeAsync(PartyRoleType.Zoo, DateTime.Today);

            var zoo = zoos.Single(x => x.Code == "KKOZ");

            var animals = await service.GetToPartiesFromPartyByRelationshipPartyRoleTypeCodeAsync(zoo.PartyId.Value,
                RelationshipPartyType.BringUp, DateTime.Today);
        }

        [Fact]
        public async Task GetZoos()
        {

            var service = new PartiesService(_testDbContextFactory);
            var zoos = await service.GetOrganizationByRoleTypeCodeAsync(PartyRoleType.Zoo, DateTime.Today);

            var zoo = zoos.Single(x => x.Code == "KKOZ");

            await service.SaveOrganizationAsync(zoo.PartyId.Value, new Parties.IServices.Dtos.OrganizationDto
            {
                Code = zoo.Code,
                Name = "Khao Kheow Open Zoo",
            });
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