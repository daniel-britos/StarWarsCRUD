using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarWarsCRUD.Domain.Entities;

namespace StarWarsCRUD.Infrastructure.Data.Configurations;

public class PersonajeConfiguration : IEntityTypeConfiguration<Personaje>
{
    public void Configure(EntityTypeBuilder<Personaje> builder)
    {
        // Configuración de Fluent Api para entidad Personaje
        builder.HasKey(p => p.Id);

       // builder.HasKey(p => p.Nombre).IsRequired().HasMaxLength(100); opcional agregar restricciones.
       
        builder.Property(p => p.RowVersion)
            .IsRowVersion();

        // 1. Relación 1:N (con Planeta) - Lado 'Muchos'
        // opcional: configurarla de esta manera o en la clase
        builder.Property(p => p.PlanetaNatalId).IsRequired();

        // 2. Relación 1:N (con Nave)
        builder.HasMany(p => p.Naves)
            .WithOne(n => n.Piloto)
            .HasForeignKey(n => n.PilotoId) // Usa la FK explícita en Nave
            .OnDelete(DeleteBehavior.SetNull); // O SetNull, o Restrict, según tu regla de negocio

        // 3. Relación N:N (con Pelicula)        
        builder.HasMany(p => p.Peliculas)
            .WithMany(m => m.Personajes)
            .UsingEntity(j => j.ToTable("AparicionesEnPeliculas")); // Nombre explícito para la tabla de unión
    }
}
