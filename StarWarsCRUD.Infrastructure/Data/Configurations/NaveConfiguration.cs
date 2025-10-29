using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarWarsCRUD.Domain.Entities;

namespace StarWarsCRUD.Infrastructure.Data.Configurations;

public class NaveConfiguration : IEntityTypeConfiguration<Nave>
{
    public void Configure(EntityTypeBuilder<Nave> builder)
    {
        builder.HasKey(n => n.Id);
        
        builder.Property(n => n.RowVersion)
            .IsRowVersion();

        // Relación N:1 (con Personaje) - Lado 'Muchos'
        // Una Nave tiene un Piloto
        builder.HasOne(n => n.Piloto)
            .WithMany(p => p.Naves)
            .HasForeignKey(n => n.PilotoId)
            .IsRequired(false); // Podrías permitir que una nave exista sin piloto al inicio (PilotoId puede ser null)

        // El tipo de enum 'TipoNave' se mapea por defecto a int.
    }
}
