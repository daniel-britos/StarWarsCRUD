
using Microsoft.EntityFrameworkCore;
using StarWarsCRUD.Domain.Entities;
using StarWarsCRUD.Domain.Interfaces;
using StarWarsCRUD.Infrastructure.Data;

namespace StarWarsCRUD.Infrastructure.Repositories;

public class PersonajeRepository : IPersonajeRepository
{
    private readonly ApplicationDbContext _dbContext;

    public PersonajeRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Personaje>> GetAllAsync()
    {
        return await _dbContext.Personajes
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Personaje?> GetByIdAsync(int id)
    {
        return await _dbContext.Personajes
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Personaje personaje)
    {
        await _dbContext.Personajes.AddAsync(personaje);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _dbContext.Personajes.AnyAsync(p => p.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}