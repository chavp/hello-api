using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MooDeng.Parties.Models;

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
                var mooDeng = new Animal("MooDeng");
                db.Add(mooDeng);
                var petRoleType = db.PartyRoleTypes.Single(x => x.Code == PartyRoleType.Pet);
                var mooDengPet = new PartyRole(petRoleType, mooDeng);
                db.Add(mooDengPet);

                var zoo = new Organization("Khao Kheow Open Zoo");
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
        public void GetPetsFromZoo()
        {
            using (var db = new PartiesContext(_partiesBuilder.Options))
            {
                var zoo = (from z in db.Parties.OfType<Organization>()
                          join pr in db.PartyRoles on z equals pr.Party
                          where pr.PartyRoleType.Code == PartyRoleType.Zoo
                          && pr.EffectiveDateTime <= DateTime.Today
                           && DateTime.Today <= pr.ExpiryDateTime
                           && z.Name == "Khao Kheow Open Zoo"
                           select z).Single();

                var pets = (from p in db.Parties.OfType<Animal>()
                           join pr in db.PartyRoles on p equals pr.Party
                           join prr in db.RelationshipPartyRoles on pr equals prr.ToPartyRole
                           where pr.PartyRoleType.Code == PartyRoleType.Pet
                           && pr.EffectiveDateTime <= DateTime.Today
                           && DateTime.Today <= pr.ExpiryDateTime
                           && prr.EffectiveDateTime <= DateTime.Today
                           && DateTime.Today <= prr.ExpiryDateTime
                           && prr.FromPartyRole.PartyId == zoo.Id
                            select p).ToList();

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