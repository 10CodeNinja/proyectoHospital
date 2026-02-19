using Hosp.Controllers.dto;
using Hosp.Models;
using Hosp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hosp.Controllers
{
    [ApiController]
    [Route("/paciente")]
    public class PacienteController :ControllerBase
    {
        private readonly PacienteServicio _paciente;

        public PacienteController(PacienteServicio paciente)
        {
            _paciente = paciente;
        }

        [HttpPost("{id}")]
        public IActionResult CrearPaciente([FromBody]PacienteDto datos, int id)
        {
            Paciente paciente = _paciente.AgregarPaciente(datos, id);
            return Ok();
        }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            return Ok();

        }


        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            return Ok();

        }

        [HttpPost("{id}")]
        public IActionResult Actualizar([FromBody] PacienteDto datos, int id)
        {
            return Ok();

        }
    }
}
