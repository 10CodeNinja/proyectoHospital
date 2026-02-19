using System;
using System.Collections.Generic;

namespace Hosp.Models;

public partial class Medico
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string Especialidad { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public DateOnly FechanNacimiento { get; set; }

    public DateOnly? FechaCreacion { get; set; }

    public virtual ICollection<Paciente> Pacientes { get; set; } = new List<Paciente>();
}
