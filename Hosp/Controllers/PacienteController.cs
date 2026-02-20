using Hosp.Controllers.dto;
using Hosp.Models;
using Hosp.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Hosp.Controllers
{
    [ApiController]
    [Route("paciente")]
    public class PacienteController :ControllerBase
    {
        private readonly PacienteServicio _paciente;

        public PacienteController(PacienteServicio paciente)
        {
            _paciente = paciente;
        }

        [HttpPost("crear/{id}")]
        public IActionResult CrearPaciente([FromBody]PacienteDto datos, int id)
        {
            Paciente paciente = _paciente.AgregarPaciente(datos, id);
            return Ok();
        }



        [HttpGet("{id}")]
        public IActionResult ObtenerPacienteConMedico(int id)
        {
            try
            {
                var paciente = _paciente.ObtenerPacienteConMedico(id);
                return Ok(paciente);
            }
            catch (Exception ex)
            {
                return NotFound(new { mensaje = ex.Message });
            }
        }

        [HttpPost("importar-csv")]
        public async Task<IActionResult> ImportarCsv([FromForm(Name = "archivo")] IFormFile archivo)
        {
            try
            {
                await _paciente.ImportarPacientesCsvAsync(archivo);
                return Ok("Pacientes importados correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
