using System;
using System.Collections.Generic;

namespace Hosp.Models;

public partial class Paciente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public int? IdMedico { get; set; }

    public DateOnly? FechaCreacion { get; set; }

    public virtual Medico? IdMedicoNavigation { get; set; }
}
