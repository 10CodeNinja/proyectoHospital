using CsvHelper;
using Hosp.Controllers.dto;
using Hosp.Excepciones;
using Hosp.Models;
using Hosp.Repositorio;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hosp.Services
{
    public class PacienteServicio
    {

        private readonly PacienteRepositorio pacienteRepositorio;
        private readonly MedicoRepositorio _medicoRepositorio;

        public PacienteServicio(PacienteRepositorio pacienteRepositorio, MedicoRepositorio medicoRepositorio)
        {
            this.pacienteRepositorio = pacienteRepositorio;
            _medicoRepositorio = medicoRepositorio;
        }

        public Paciente AgregarPaciente(PacienteDto datos, int id)
        {

            Medico medico = _medicoRepositorio.ObtenerPorId(id);
            if (medico == null)
                throw new NotFoundException($"No se encontro el medico con id {id}");


            Paciente nuevoPaciente = new Paciente
            {
                Nombre = datos.nombre.ToLower(),
                Telefono = datos.telefono.ToLower(),
                Correo = datos.correo.ToLower(),
                IdMedico = medico.Id,
                IdMedicoNavigation = medico
            };

            int afectada = pacienteRepositorio.AgregarPaciente(nuevoPaciente);

            if (afectada == 0)
                throw new NotFoundException("No se pudo agregar el paciente");

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
                throw new Exception("Archivo CSV vacio");

            using var reader = new StreamReader(archivo.OpenReadStream());
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var pacientesCsv = csv.GetRecords<PacienteCsvModel>().ToList();

            var pacientes = new List<Paciente>();

            foreach (var p in pacientesCsv)
            {
                // Verificar que el médico existe
                var medicoExiste = _medicoRepositorio.ObtenerPorId(p.IdMedico);
                if (medicoExiste == null)
                    throw new MedicoNoEncontradoException(p.IdMedico);

                // Crear paciente
                pacientes.Add(new Paciente
                {
                    Nombre = p.Nombre,
                    Telefono = p.Telefono,
                    Correo = p.Correo,
                    IdMedico = p.IdMedico
                });
            }

            try
            {
                await pacienteRepositorio.AgregarPacientesAsync(pacientes);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2627)
            {
                // Manejo de duplicados (violación UNIQUE KEY)
                throw new Exception("Error: Algunos correos electronicos ya existen en la base de datos.");
            }
        }
    }
}
