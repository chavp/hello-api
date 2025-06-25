using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MooDeng.Parties.Mappings;
using MooDeng.Parties.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Api.Tests
{
    public class FacilitiesTests
    {
        protected readonly IConfigurationRoot _config = null;
        protected readonly DbContextOptionsBuilder<PartiesContext> _partiesBuilder = null;
        protected readonly PartiesDbContextFactory _testDbContextFactory;

        public FacilitiesTests()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            _partiesBuilder = new DbContextOptionsBuilder<PartiesContext>()
                .UseSqlServer(_config.GetConnectionString("parties_db"));

            _testDbContextFactory = new PartiesDbContextFactory(_config.GetConnectionString("parties_db"));
        }

        [Fact]
        public void AddFacilityType()
        {
            using var db = _testDbContextFactory.CreateDbContext();

            var ft1 = new FacilityType(FacilityType.Industrial);
            db.Add(ft1);

            var ft2 = new FacilityType(FacilityType.Commercial);
            db.Add(ft2);

            var ft3 = new FacilityType(FacilityType.Residential);
            db.Add(ft3);

            var ft4 = new FacilityType(FacilityType.Institutional);
            db.Add(ft4);

            var ft5 = new FacilityType(FacilityType.Recreational);
            db.Add(ft5);

            db.SaveChanges();
        }

        [Fact]
        public void AddLands()
        {
            using var db = _testDbContextFactory.CreateDbContext();

            var recreational = db.FacilityTypes.Single(x => x.Code == FacilityType.Recreational);
            var l1 = new Land(recreational, "L001");
            db.Add(l1);

            db.SaveChanges();
        }
    }
}
