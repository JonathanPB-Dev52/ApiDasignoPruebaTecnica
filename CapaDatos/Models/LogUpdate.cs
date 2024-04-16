using System;
using System.Collections.Generic;

namespace CapaDatos.Models;

public partial class LogUpdate
{
    public int Id { get; set; }

    public int? IdPersona { get; set; }

    public string? ObjPersona { get; set; }

    public DateTime? FechaActualizacion { get; set; }
}
