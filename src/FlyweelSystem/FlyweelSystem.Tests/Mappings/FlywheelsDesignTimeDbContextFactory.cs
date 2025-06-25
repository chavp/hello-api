using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyweelSystem.Tests.Mappings
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
