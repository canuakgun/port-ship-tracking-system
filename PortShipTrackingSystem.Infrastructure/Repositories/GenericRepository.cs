namespace PortShipTrackingSystem.Infrastructure.Repositories;

using PortShipTrackingSystem.Core.Exceptions;
using PortShipTrackingSystem.Core.Interfaces;
using PortShipTrackingSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using PortShipTrackingSystem.Core.Entities;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync(int id)
{
    return await _context.Set<T>().FindAsync(id);
}
public async Task<IEnumerable<T>> GetAllAsync()
{
    return await _context.Set<T>().ToListAsync();
}
public async Task AddAsync(T entity)
{
    await _context.Set<T>().AddAsync(entity);
}
public void Update(T entity, string currentUsername)
{
    entity.UpdatedAt = DateTime.UtcNow;
    entity.UpdatedBy = currentUsername;
    _context.Set<T>().Update(entity);
}
public void Delete(T entity, string currentUsername)
{
    entity.IsDeleted = true;
    entity.UpdatedAt = DateTime.UtcNow;
    entity.UpdatedBy = currentUsername;
    _context.Set<T>().Update(entity);
}
public async Task<bool> SaveChangesAsync()
{
    try
    {
        return await _context.SaveChangesAsync() > 0;
    }
    catch (DbUpdateConcurrencyException)
    {
        throw new ConcurrencyException("The record was modified by another user. Please reload and try again.");
    }
}
}