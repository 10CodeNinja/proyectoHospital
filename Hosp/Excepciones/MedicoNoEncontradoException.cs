using Hosp.Models;

namespace Hosp.Excepciones
{
    public class MedicoNoEncontradoException : Exception
    {

        public MedicoNoEncontradoException(int id) :base($"No se encontro el medico con id {id}") { }
    }
}
