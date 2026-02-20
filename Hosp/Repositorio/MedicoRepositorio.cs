using Hosp.Controllers.dto;
using Hosp.Models;
using Hosp.Services;

namespace Hosp.Repositorio
{
    public class MedicoRepositorio
    {
        private HospitalContext _HospitalContext;

        public MedicoRepositorio(HospitalContext HospitalContext)
        {
            _HospitalContext = HospitalContext;
        }
        public Medico ObtenerPorId(int id)
        {
            return _HospitalContext.Medicos.Find(id);

        }

        public int AgregarMedico(Medico medico)
        {
            _HospitalContext.Medicos.Add(medico);
            int afectada = _HospitalContext.SaveChanges();
            return afectada;
        }

        public int EliminarMedico(Medico medico)
        {
            _HospitalContext.Medicos.Remove(medico);
            int affectada = _HospitalContext.SaveChanges();
            return affectada;
        }

        public List<Medico> ObtenerTodos()
        {
            return _HospitalContext.Medicos.ToList();
        }

        public int ActualizarMedico(Medico medico)
        {
            _HospitalContext.Medicos.Update(medico);
            int afectada = _HospitalContext.SaveChanges();

            return afectada;
        }

        public int eliminarMedico(Medico medico)
        {
            _HospitalContext.Medicos.Remove(medico);
            int afectada = _HospitalContext.SaveChanges();

            return afectada;
        }

        


    }
}
