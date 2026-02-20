namespace Hosp.Controllers.dto
{
    public class PacienteCsvModel
    {
        public string Nombre { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public int IdMedico { get; set; } 
    }
}
