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
        public DbSet<User> Users { get; set; }

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

        modelBuilder.Entity<Ship>().HasQueryFilter(s => !s.IsDeleted);
        modelBuilder.Entity<Port>().HasQueryFilter(p => !p.IsDeleted);
        modelBuilder.Entity<ShipVisit>().HasQueryFilter(v => !v.IsDeleted);
        modelBuilder.Entity<Cargo>().HasQueryFilter(c => !c.IsDeleted);
        modelBuilder.Entity<CrewMember>().HasQueryFilter(cm => !cm.IsDeleted);
        modelBuilder.Entity<ShipCrewAssignment>().HasQueryFilter(a => !a.IsDeleted);
        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
    }
    }
}