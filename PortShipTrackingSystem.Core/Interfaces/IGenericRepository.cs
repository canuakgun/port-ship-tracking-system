using PortShipTrackingSystem.Core.Entities;

namespace PortShipTrackingSystem.Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize);
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity, string currentUsername);
    void Delete(T entity, string currentUsername);
    Task<bool> SaveChangesAsync();
}