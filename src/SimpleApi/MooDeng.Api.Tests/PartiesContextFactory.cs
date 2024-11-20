using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MooDeng.Api.Tests
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
