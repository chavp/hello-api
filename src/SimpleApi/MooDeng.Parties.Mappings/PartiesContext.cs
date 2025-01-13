using Microsoft.EntityFrameworkCore;
using MooDeng.Parties.Models;


namespace MooDeng.Parties.Mappings
{
    public class PartiesContext : DbContext
    {
        public DbSet<Party> Parties { get; set; }
        public DbSet<PartyRole> PartyRoles { get; set; }
        public DbSet<PartyRoleType> PartyRoleTypes { get; set; }
        public DbSet<RelationshipPartyRole> RelationshipPartyRoles { get; set; }
        public DbSet<RelationshipPartyRoleType> RelationshipPartyRoleTypes { get; set; }
        public PartiesContext(DbContextOptions<PartiesContext> options)
            : base(options)
        {
           
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("parties");

            modelBuilder.Entity<Person>();
            modelBuilder.Entity<Animal>();
            modelBuilder.Entity<Organization>();

            modelBuilder
            .Entity<PartyRoleType>()
            .Property(e => e.Code)
            .HasConversion(ValueConverters.UpperConverter!);

            modelBuilder
            .Entity<RelationshipPartyRoleType>()
            .Property(e => e.Code)
            .HasConversion(ValueConverters.UpperConverter!);

            modelBuilder
           .Entity<Organization>()
           .Property(e => e.Code)
           .HasConversion(ValueConverters.UpperConverter!);
        }
    }
}
