namespace Hosp.Controllers.dto
{
    public class MedicoDto
    {
        public required string nombre { get; set; }
        public required string telefono { get; set; }
        public required string especialidad { get; set; }
        public required string correo { get; set; }

        public required DateOnly fechaNacimiento { get; set; }

    }
}
