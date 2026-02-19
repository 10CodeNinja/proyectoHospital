using Hosp.Controllers.dto;
using Hosp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hosp.Controllers
{

    [ApiController]
    [Route("/medico")]
    public class MedicoController: ControllerBase
    {
        private readonly MedicoServicio _medicoServicio;

        public MedicoController(MedicoServicio medicoServicio)
        {
            _medicoServicio = medicoServicio;
        }

        [HttpPost]
        public IActionResult CrearMedico(MedicoDto datos)
        {
            return Ok();
        }
        [HttpGet]
        public IActionResult ObtenerTodo()
        {
            return Ok();

        }
        
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            return Ok();

        }
        [HttpPost("{id}")]
        public IActionResult Actualizar([FromBody]MedicoDto datos,int id)
        {
            return Ok();

        }
    }
}
