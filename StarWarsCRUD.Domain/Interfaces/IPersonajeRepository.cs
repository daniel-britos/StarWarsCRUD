using StarWarsCRUD.Domain.Entities;

namespace StarWarsCRUD.Domain.Interfaces;
public interface IPersonajeRepository
{
    Task<IEnumerable<Personaje>> GetAllAsync();
    Task<Personaje?> GetByIdAsync(int id);
    Task AddAsync(Personaje personaje);
    Task<bool> ExistsAsync(int id);
    Task SaveChangesAsync();
}