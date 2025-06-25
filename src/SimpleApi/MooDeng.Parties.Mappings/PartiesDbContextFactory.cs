using Microsoft.EntityFrameworkCore;
using MooDeng.Parties.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Api.Tests
{
    /// <summary>
    /// https://medium.com/@yayasaafan/mastering-dbcontext-in-entity-framework-core-configuration-lifetime-and-best-practices-e8e89037d34d
    /// </summary>
    public class PartiesDbContextFactory : IDbContextFactory<PartiesContext>
    {
        private DbContextOptions<PartiesContext> _options;

        public PartiesDbContextFactory(string connectionString)
        {
            _options = new DbContextOptionsBuilder<PartiesContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public PartiesContext CreateDbContext()
        {
            return new PartiesContext(_options);
        }
    }
}
