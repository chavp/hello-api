using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using MooDeng.Parties.Models;
using System.Data.Entity;

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
        public void AddRolePet()
        {
            using (var db = new PartiesContext(_partiesBuilder.Options))
            {
                var pet = new PartyRoleType(PartyRoleType.Pet);
                db.Add(pet);

                var zoo = new PartyRoleType(PartyRoleType.Zoo);
                db.Add(zoo);

                var bringUp = new RelationshipPartyRoleType(RelationshipPartyRoleType.BringUp);
                db.Add(bringUp);

                db.SaveChanges();
            }
        }

        [Fact]
        public void AddMooDeng()
        {
            using (var db = new PartiesContext(_partiesBuilder.Options))
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

                var bringUpType = db.RelationshipPartyRoleTypes.Single(x => x.Code == RelationshipPartyRoleType.BringUp);
                var re = new RelationshipPartyRole(bringUpType, mooDengZoo, mooDengPet);
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
            using (var db = new PartiesContext(_partiesBuilder.Options))
            {

                var service = new PartiesService(db);
                var zoos = await service.GetOrganizationByRoleTypeCodeAsync(PartyRoleType.Zoo, DateTime.Today);

                var zoo = zoos.Single(x => x.PartyCode == "KKOZ");

                var animals = await service.GetToPartiesFromPartyByRelationshipPartyRoleTypeCodeAsync(zoo.PartyId,
                    RelationshipPartyRoleType.BringUp, DateTime.Today);
            }
        }

        [Fact]
        public async Task GetZoos()
        {

            using (var db = new PartiesContext(_partiesBuilder.Options))
            {
                var service = new PartiesService(db);
                var zoos = await service.GetOrganizationByRoleTypeCodeAsync(PartyRoleType.Zoo, DateTime.Today);

                var zoo = zoos.Single(x => x.PartyCode == "KKOZ");

                await service.SaveOrganizationAsync(zoo.PartyId, new Parties.IServices.Dto.OrganizationDataDto
                {
                    PartyCode = zoo.PartyCode,
                    PartyName = "Khao Kheow Open Zoo test",
                });

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