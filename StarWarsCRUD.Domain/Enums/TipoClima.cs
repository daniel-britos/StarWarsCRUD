using System;

namespace StarWarsCRUD.Domain.Enums;

public enum TipoClima
{
    Templado,
    Arido,
    Tropical,
    Artico,
    Volcanico
}

/* 
 
 •	Seguridad de tipo: evita valores inválidos en las entidades y errores en tiempo de compilación.
•	Documentación y restricción: centraliza y deja explícitas las opciones válidas del dominio.
•	Legibilidad y mantenimiento: reemplaza magic strings/números por nombres claros.
•	Facilita validación y lógica: simplifica switch/pattern matching y reglas de negocio.
•	Mapeo/serialización controlada: hace más predecible el almacenamiento y la conversión a DTOs/JSON
 
 
 
 */