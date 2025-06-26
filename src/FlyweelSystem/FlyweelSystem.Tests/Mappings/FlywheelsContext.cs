using FlyweelSystem.Tests.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FlyweelSystem.Tests.Mappings
{
    public class FlywheelsContext : DbContext
    {
        public DbSet<Boundary> Boundaries { get; set; }
        public DbSet<Element> Elements { get; set; }
        public DbSet<ElementType> ElementTypes { get; set; }
        public DbSet<PartyType> PartyTypes { get; set; }

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
                .Entity<ElementType>()
                .Property(e => e.Code)
                .HasConversion(ValueConverters.UpperConverter!);

            modelBuilder
                .Entity<PartyType>()
                .Property(e => e.Code)
                .HasConversion(ValueConverters.UpperConverter!);

            modelBuilder
                .Entity<ElementRelationshipType>()
                .Property(e => e.Code)
                .HasConversion(ValueConverters.UpperConverter!);

            //modelBuilder
            //    .Entity<ContextRelationship>()
            //    .HasOne(e => e.FromElement)
            //    .WithOne(e => e.FromContextRelationship)
            //    .OnDelete(DeleteBehavior.ClientNoAction);

            //modelBuilder
            //    .Entity<ContextRelationship>()
            //    .HasOne(e => e.ToElement)
            //    .WithOne(e => e.ToContextRelationship)
            //    .OnDelete(DeleteBehavior.ClientNoAction);

        }
    }
}
