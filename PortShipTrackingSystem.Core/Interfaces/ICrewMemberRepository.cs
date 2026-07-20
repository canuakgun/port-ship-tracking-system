namespace PortShipTrackingSystem.Core.Interfaces;

using PortShipTrackingSystem.Core.Entities;

public interface ICrewMemberRepository : IGenericRepository<CrewMember>
{
    Task<CrewMember?> GetByEmailAsync(string email);
}