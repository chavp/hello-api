using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyweelSystem.Tests.Mappings
{
    public class FlywheelsDbContextFactory : IDbContextFactory<FlywheelsContext>
    {
        private DbContextOptions<FlywheelsContext> _options;

        public FlywheelsDbContextFactory(string connectionString)
        {
            _options = new DbContextOptionsBuilder<FlywheelsContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public FlywheelsContext CreateDbContext()
        {
            return new FlywheelsContext(_options);
        }
    }
}
