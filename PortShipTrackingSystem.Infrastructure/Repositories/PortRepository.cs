namespace PortShipTrackingSystem.Infrastructure.Repositories;

using PortShipTrackingSystem.Core.Entities;
using PortShipTrackingSystem.Core.Interfaces;
using PortShipTrackingSystem.Infrastructure.Data;

public class PortRepository : GenericRepository<Port>, IPortRepository
{
    public PortRepository(AppDbContext context) : base(context)
    {
    }
}