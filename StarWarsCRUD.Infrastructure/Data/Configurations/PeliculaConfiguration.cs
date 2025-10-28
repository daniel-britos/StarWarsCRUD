using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarWarsCRUD.Domain.Entities;

namespace StarWarsCRUD.Infrastructure.Data.Configurations;

public class PeliculaConfiguration : IEntityTypeConfiguration<Pelicula>
{
    public void Configure(EntityTypeBuilder<Pelicula> builder)
    {
        builder.HasKey(m => m.Id);

        // Concurrencia (RowVersion)
        builder.Property(m => m.RowVersion)
            .IsRowVersion();

        // Mapeo del tipo de dato DateOnly a la base de datos
        // (EF Core 9 lo soporta nativamente, pero es buena práctica indicarlo si usas versiones anteriores)
        builder.Property(m => m.FechaEstreno)
            .HasColumnType("date");

        // Relación N:N (con Personaje)
        // Ya configurada desde PersonajeConfiguration, solo reconfirmamos la relación.
        builder.HasMany(m => m.Personajes)
            .WithMany(p => p.Peliculas)
            .UsingEntity(j => j.ToTable("AparicionesEnPeliculas"));
    }
}
