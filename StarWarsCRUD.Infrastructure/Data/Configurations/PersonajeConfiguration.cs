using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarWarsCRUD.Domain.Entities;

namespace StarWarsCRUD.Infrastructure.Data.Configurations;

public class PersonajeConfiguration : IEntityTypeConfiguration<Personaje>
{
    public void Configure(EntityTypeBuilder<Personaje> builder)
    {
        builder.HasKey(p => p.Id);

        /*
         *Podemos configurar restriccionaes en esta parte en lugar de la clase si deseamos.
         *Es recomendable mantener las validaciones en la clase para pequeños proyectos como este
         *para medianos y grandes proyectos es recomendable hacerlo en el modelbuilder de esta manera
         *
           builder.HasKey(p => p.Nombre).IsRequired().HasMaxLength(100);
        */

        // Concurrencia (RowVersion)
        builder.Property(p => p.RowVersion)
            .IsRowVersion();

        // 1. Relación 1:N (con Planeta) - Lado 'Muchos'
        // Ya configurada desde Planeta, solo necesitamos asegurarnos de que la FK es requerida.
        builder.Property(p => p.PlanetaNatalId).IsRequired();

        // 2. Relación 1:N (con Nave) - Lado 'Uno'
        // Un Personaje (Piloto) tiene muchas Naves
        builder.HasMany(p => p.Naves)
            .WithOne(n => n.Piloto)
            .HasForeignKey(n => n.PilotoId) // Usa la FK explícita en Nave
            .OnDelete(DeleteBehavior.SetNull); // O SetNull, o Restrict, según tu regla de negocio

        // 3. Relación N:N (con Pelicula)
        // Usa la convención de nombres para la tabla de unión implícita
        builder.HasMany(p => p.Peliculas)
            .WithMany(m => m.Personajes)
            .UsingEntity(j => j.ToTable("AparicionesEnPeliculas")); // Nombre explícito para la tabla de unión
    }
}
