using Hosp.Controllers.dto;
using Hosp.Excepciones;
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
        public IActionResult CrearPaciente([FromBody] PacienteDto datos, int id)
        {
            try
            {
                Paciente paciente = _paciente.AgregarPaciente(datos, id);
                return Ok(paciente); 
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { mensaje = ex.Message }); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
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
        public async Task<IActionResult> ImportarCsv( IFormFile archivo)
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
