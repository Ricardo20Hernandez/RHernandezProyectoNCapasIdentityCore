using System;
using System.Collections.Generic;

namespace DL;

public partial class DependienteTipo
{
    public int IdDependienteTipo { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<Dependiente> Dependientes { get; set; } = new List<Dependiente>();
}
