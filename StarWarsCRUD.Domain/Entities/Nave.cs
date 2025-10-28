using StarWarsCRUD.Domain.Enums;

namespace StarWarsCRUD.Domain.Entities;

public class Nave
{
    public int Id { get; private set; }    
    public TipoNave? TipoNave { get; private set; }

    // --- Relación N:1 (cada nave pertenece a un personaje/piloto) ---
    public int? PilotoId { get; private set; }
    public Personaje? Piloto { get; private set; }
    public byte[]? RowVersion { get; private set; }

    private Nave() { }

    // El constructor debe aceptar un piloto opcional si permites la nulabilidad
    public Nave(Personaje? piloto = null, TipoNave? tipoNave = null)
    {        
        TipoNave = tipoNave;

        // Si el piloto se proporciona, sincronizamos. Lado piloto
        if (piloto != null)
        {
            piloto.AgregarNave(this);
        }
    }

    public void ActualizarTipoNave(TipoNave nuevoTipo)
    {
        TipoNave = nuevoTipo;
    }
}

