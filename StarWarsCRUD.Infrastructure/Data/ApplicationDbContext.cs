using Microsoft.EntityFrameworkCore;
using StarWarsCRUD.Domain.Entities;

namespace StarWarsCRUD.Infrastructure.Data;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Personaje> Personajes { get; set; }
}
