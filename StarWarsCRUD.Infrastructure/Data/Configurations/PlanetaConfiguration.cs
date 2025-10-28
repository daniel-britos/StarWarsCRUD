using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarWarsCRUD.Domain.Entities;

namespace StarWarsCRUD.Infrastructure.Data.Configurations;

public class PlanetaConfiguration : IEntityTypeConfiguration<Planeta>
{
    public void Configure(EntityTypeBuilder<Planeta> builder)
    {
        // Clave Primaria
        builder.HasKey(p => p.Id);

        // Concurrencia (RowVersion)
        builder.Property(p => p.RowVersion)
            .IsRowVersion(); // Define esta columna como un token de concurrencia

        // Relación 1:N con Personaje
        // Un Planeta tiene muchos PersonajesNativos
        builder.HasMany(p => p.PersonajesNativos)
            .WithOne(pe => pe.PlanetaNatal)
            .HasForeignKey(pe => pe.PlanetaNatalId) // Usa la FK explícita en Personaje
            .OnDelete(DeleteBehavior.Restrict); // Evita borrados en cascada por defecto (mejor control)
    }
}
