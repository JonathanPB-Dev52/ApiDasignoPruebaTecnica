using System;
using System.Collections.Generic;

namespace CapaDatos.Models;

public partial class Persona
{
    public int Id { get; set; }

    public string? PrimerNombre { get; set; }

    public string? SegundoNombre { get; set; }

    public string? PrimerApellido { get; set; }

    public string? SegundoApellido { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public int? Sueldo { get; set; }

    public DateTime? FechaCreacion { get; set; }
}
