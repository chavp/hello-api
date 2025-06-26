using FlyweelSystem.Tests.Mappings;
using FlyweelSystem.Tests.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyweelSystem.Tests
{
    public class C4Samples : TestBase
    {
        [Fact]
        public void SeedContext()
        {
            var nspAls = "C4";
            using (var db = _sutDbContextFactory.CreateDbContext())
            using (var tran = db.Database.BeginTransaction())
            {
                var sysCtx = saveElement(db, nspAls, ElementType.Context, "InternetBankingContext", "Internet Banking Context System");

                var customer = saveElement(db, nspAls, ElementType.System, "PersonalBankingCustomer", "Personal Banking Customer");
            }
        }
    }
}
