using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarWarsCRUD.Domain.Entities;

public class Nave
{
    public int Id { get; private set; }
    public string Nombre { get; private set; }
    public string Modelo { get; private set; }

    // --- Relación N:1 (cada nave pertenece a un personaje/piloto) ---
    public int PilotoId { get; private set; }
    public Personaje Piloto { get; private set; }

    // Concurrency token opcional
    public byte[]? RowVersion { get; private set; }

    private Nave() { }

    public Nave(string nombre, string modelo, Personaje piloto)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre de la nave no puede estar vacío.", nameof(nombre));

        if (string.IsNullOrWhiteSpace(modelo))
            throw new ArgumentException("El modelo no puede estar vacío.", nameof(modelo));

        Piloto = piloto ?? throw new ArgumentNullException(nameof(piloto));
        Nombre = nombre;
        Modelo = modelo;

        // Sincronización 1:N (lado piloto)
        piloto.AgregarNave(this);
    }

    public void ActualizarModelo(string nuevoModelo)
    {
        if (string.IsNullOrWhiteSpace(nuevoModelo))
            throw new ArgumentException("El modelo no puede estar vacío.", nameof(nuevoModelo));

        Modelo = nuevoModelo;
    }
}

