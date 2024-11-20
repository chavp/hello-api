using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using static System.Net.Mime.MediaTypeNames;
using System.Data;
using System.Security.Claims;
using MooDeng.Api.Tests.Models;


namespace MooDeng.Api.Tests
{
    public class PartiesContext : DbContext
    {
        public PartiesContext(DbContextOptions<PartiesContext> options)
            : base(options)
        {
           
        }
        public DbSet<Party> Parties { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("parties");

            modelBuilder.Entity<Person>();

        }
    }
}
