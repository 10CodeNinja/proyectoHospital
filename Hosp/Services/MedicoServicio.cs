using Hosp.Controllers.dto;
using Hosp.Models;
using Hosp.Repositorio;

namespace Hosp.Services
{
    public class MedicoServicio
    {

        private readonly MedicoRepositorio _medicoRepositorio;

        public MedicoServicio(MedicoRepositorio medicoRepositorio)
        {
            _medicoRepositorio = medicoRepositorio;
        }


        public Medico AgregarMedico(MedicoDto datos)
        {
            Medico nuevoMedico = new Medico();
            nuevoMedico.Nombre = datos.nombre.ToLower();
            nuevoMedico.Telefono = datos.telefono.ToLower();
            nuevoMedico.Correo = datos.correo.ToLower();
            nuevoMedico.Especialidad = datos.especialidad.ToLower();
            nuevoMedico.FechanNacimiento = datos.fechaNacimiento;

            int afectada = _medicoRepositorio.AgregarMedico(nuevoMedico);

            if (afectada == 0) throw new Exception("No se ingreso");

            return nuevoMedico;


        }

        public Medico ObtenerPorId(int id)
        {
            Medico existe = _medicoRepositorio.ObtenerPorId(id);

            if (existe == null) throw new Exception("El medico no existe");

            return existe;
        }
    }
}
