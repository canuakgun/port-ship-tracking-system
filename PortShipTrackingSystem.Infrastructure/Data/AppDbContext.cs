using Microsoft.EntityFrameworkCore;
using PortShipTrackingSystem.Core.Entities;

namespace PortShipTrackingSystem.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Ship> Ships { get; set; }
        public DbSet<Port> Ports { get; set; }
        public DbSet<ShipVisit> ShipVisits { get; set; }
        public DbSet<Cargo> Cargoes { get; set; }
        public DbSet<CrewMember> CrewMembers { get; set; }
        public DbSet<ShipCrewAssignment> ShipCrewAssignments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ship>()
            .HasIndex(s => s.IMO)
            .IsUnique();

        modelBuilder.Entity<ShipCrewAssignment>()
            .HasIndex(a => new { a.ShipId, a.CrewId, a.AssignmentDate })
            .IsUnique();

        modelBuilder.Entity<CrewMember>()
            .HasKey(x => x.CrewId);
        
        modelBuilder.Entity<ShipVisit>()
            .HasKey(x => x.VisitId);

        modelBuilder.Entity<ShipCrewAssignment>()
            .HasKey(x => x.AssignmentId);
    }
    }
}