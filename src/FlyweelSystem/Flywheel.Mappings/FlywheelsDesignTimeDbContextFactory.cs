using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Flywheel.Mappings
{
    public class FlywheelsDesignTimeDbContextFactory : IDesignTimeDbContextFactory<FlywheelsContext>
    {
        public FlywheelsContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FlywheelsContext>();
            optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=flywheels_db;TrustServerCertificate=True;User Id=sa;Password=mflv[@1234b;");
            return new FlywheelsContext(optionsBuilder.Options);
        }
    }
}
