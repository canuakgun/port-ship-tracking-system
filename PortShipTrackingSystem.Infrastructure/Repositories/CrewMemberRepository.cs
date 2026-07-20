namespace PortShipTrackingSystem.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;
using PortShipTrackingSystem.Core.Entities;
using PortShipTrackingSystem.Core.Interfaces;
using PortShipTrackingSystem.Infrastructure.Data;

public class CrewMemberRepository : GenericRepository<CrewMember>, ICrewMemberRepository
{
    private readonly AppDbContext _context;

    public CrewMemberRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<CrewMember?> GetByEmailAsync(string email)
    {
        return await _context.CrewMembers.FirstOrDefaultAsync(c => c.Email == email);
    }
}