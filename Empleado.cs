using System;
using System.Collections.Generic;

namespace netpostgres;

public partial class Empleado
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public string? Apellidos { get; set; }

    public int? Edad { get; set; }
}
