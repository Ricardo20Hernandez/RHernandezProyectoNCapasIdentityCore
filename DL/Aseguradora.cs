using System;
using System.Collections.Generic;

namespace DL;

public partial class Aseguradora
{
    public int IdAseguradora { get; set; }

    public string Nombre { get; set; } = null!;

    public DateTime? FechaCreacion { get; set; }

    public DateTime? FechaModificación { get; set; }

    public string? Id { get; set; }

    public virtual AspNetUser? IdNavigation { get; set; }
}
