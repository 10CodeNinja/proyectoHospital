using Hosp.Controllers.dto;
using Hosp.Models;
using Hosp.Services;
using Microsoft.EntityFrameworkCore;

namespace Hosp.Repositorio
{
    public class PacienteRepositorio
    {

        private HospitalContext _hospitalContext;

        public PacienteRepositorio(HospitalContext hospitalContext)
        {
            _hospitalContext = hospitalContext;
        }


        public Paciente ObtenerPorId(int id)
        {
            return _hospitalContext.Pacientes.Find(id);
        }

        public int AgregarPaciente(Paciente paciente)
        {
            _hospitalContext.Pacientes.Add(paciente);
            int afectada = _hospitalContext.SaveChanges();
            return afectada;
        }

        public int eliminarPaciente(Paciente paciente)
        {
            _hospitalContext.Pacientes.Remove(paciente);
            int afectada = _hospitalContext.SaveChanges();
            return afectada;
        }

        public List<Paciente> ObtenerTodos()
        {
            return _hospitalContext.Pacientes.ToList();
        }

        public int ActualizarMedico(Paciente paciente)
        {
            _hospitalContext.Pacientes.Update(paciente);
            int afectada = _hospitalContext.SaveChanges();
            return afectada;
        }

        public PacienteConMedicoDto? ObtenerPacienteConMedico(int id)
        {
            var paciente = _hospitalContext.Pacientes
                                           .Include(p => p.IdMedicoNavigation) 
                                           .FirstOrDefault(p => p.Id == id);

            if (paciente == null) return null;

            
            return new PacienteConMedicoDto
            {
                NombrePaciente = paciente.Nombre,
                Telefono = paciente.Telefono,
                Correo = paciente.Correo,
                NombreMedico = paciente.IdMedicoNavigation?.Nombre ?? "Sin médico asignado"
            };
        }
    }
}
