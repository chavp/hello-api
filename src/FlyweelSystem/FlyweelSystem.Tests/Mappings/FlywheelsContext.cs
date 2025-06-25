using FlyweelSystem.Tests.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyweelSystem.Tests.Mappings
{
    public class FlywheelsContext : DbContext
    {
        public DbSet<Element> Elements { get; set; }
        public DbSet<ElementType> ElementTypes { get; set; }

        public DbSet<ElementRelationship> ElementRelationships { get; set; }
        public DbSet<ElementRelationshipType> ElementRelationshipTypes { get; set; }

        public FlywheelsContext(DbContextOptions<FlywheelsContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("flywheels");

            modelBuilder
                .Entity<Element>()
                .Property(e => e.Code)
                .HasConversion(ValueConverters.UpperConverter!);

            modelBuilder
                .Entity<ElementType>()
                .Property(e => e.Code)
                .HasConversion(ValueConverters.UpperConverter!);

            modelBuilder
                .Entity<ElementRelationshipType>()
                .Property(e => e.Code)
                .HasConversion(ValueConverters.UpperConverter!);
        }
    }
}
