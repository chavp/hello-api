using Microsoft.EntityFrameworkCore;

namespace Flywheel.Mappings
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
