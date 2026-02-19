using Hosp.Controllers.dto;
using Hosp.Models;
using Hosp.Repositorio;

namespace Hosp.Services
{
    public class PacienteServicio
    {

        private readonly PacienteRepositorio pacienteRepositorio;
        private readonly MedicoRepositorio _medicoRepositorio;

        public PacienteServicio(PacienteRepositorio pacienteRepositorio, MedicoRepositorio medicoRepositorio    )
        {
            this.pacienteRepositorio = pacienteRepositorio;
            _medicoRepositorio = medicoRepositorio;
        }

        public Paciente AgregarPaciente(PacienteDto datos, int id)
        {
            // buscar el medico
            Medico medico = _medicoRepositorio.ObtenerPorId(id);

            if (medico == null) throw new Exception("No existe el medico con ese id");
            
            Paciente nuevoPaciente = new Paciente();
            nuevoPaciente.Nombre = datos.nombre.ToLower();
            nuevoPaciente.Telefono = datos.telefono.ToLower();
            nuevoPaciente.Correo = datos.correo.ToLower();
            nuevoPaciente.IdMedico = medico.Id;

            int afectada = pacienteRepositorio.AgregarPaciente(nuevoPaciente);

            if (afectada == 0) throw new Exception("No se agrego el paciente");

            return nuevoPaciente;



        }
    }
}
