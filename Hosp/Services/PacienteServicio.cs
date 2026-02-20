using CsvHelper;
using Hosp.Controllers.dto;
using Hosp.Models;
using Hosp.Repositorio;
using System.Globalization;

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
          
            Medico medico = _medicoRepositorio.ObtenerPorId(id);

            if (medico == null) throw new Exception("No existe el medico con ese id");
            
            Paciente nuevoPaciente = new Paciente();
            nuevoPaciente.Nombre = datos.nombre.ToLower();
            nuevoPaciente.Telefono = datos.telefono.ToLower();
            nuevoPaciente.Correo = datos.correo.ToLower();
            nuevoPaciente.IdMedico = medico.Id;
            nuevoPaciente.IdMedicoNavigation = medico;

            int afectada = pacienteRepositorio.AgregarPaciente(nuevoPaciente);

            if (afectada == 0) throw new Exception("No se agrego el paciente");

            return nuevoPaciente;



        }

        public PacienteConMedicoDto ObtenerPacienteConMedico(int id)
        {
            var pacienteDto = pacienteRepositorio.ObtenerPacienteConMedico(id);

            if (pacienteDto == null)
                throw new Exception("Paciente no encontrado");

            return pacienteDto;
        }

        public async Task ImportarPacientesCsvAsync(IFormFile archivo)
        {
            if (archivo == null || archivo.Length == 0)
                throw new Exception("Archivo CSV vacío");

            using var reader = new StreamReader(archivo.OpenReadStream());
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var pacientesCsv = csv.GetRecords<PacienteCsvModel>().ToList();

           
            var pacientes = pacientesCsv.Select(p => new Paciente
            {
                Nombre = p.Nombre,
                Telefono = p.Telefono,
                Correo = p.Correo,
                IdMedico = p.IdMedico
            }).ToList();

            await pacienteRepositorio.AgregarPacientesAsync(pacientes);
        }
    }
}
