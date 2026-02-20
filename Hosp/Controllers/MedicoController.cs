using Hosp.Controllers.dto;
using Hosp.Models;
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
            Medico medico = _medicoServicio.AgregarMedico(datos);

            return Ok(medico);
        }
        
        
        [HttpDelete("{id}")]
        public IActionResult Eliminar(int id)
        {
            _medicoServicio.eliminar(id);
            return Ok();

        }
        [HttpPost("{id}")]
        public IActionResult ActualizarMedico([FromBody]MedicoDto datos,int id)
        {
           Medico medico =  _medicoServicio.Actualizar(datos, id);

            return Ok(medico);

        }

        
    }
}
