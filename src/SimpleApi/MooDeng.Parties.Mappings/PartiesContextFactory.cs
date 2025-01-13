using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MooDeng.Parties.Mappings
{
    public class PartiesContextFactory : IDesignTimeDbContextFactory<PartiesContext>
    {
        public PartiesContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PartiesContext>();
            optionsBuilder.UseSqlServer("Server=127.0.0.1;Database=parties_db;TrustServerCertificate=True;User Id=sa;Password=mflv[@1234b;");
            return new PartiesContext(optionsBuilder.Options);
        }
    }
}
