using CapaDatos.Models;
using CapaDatos.Reponse;

namespace CapaNegocio.Contracts
{
    public interface IPersona
    {
        public List<Persona> ConsultarPersonas();
        public string CrearPersona(Persona persona);
        public string EditarPersona(int Id, Persona persona);
        public string EliminarPersona(int Id);
        public List<Persona> ConsultarId(int Id);
        public (List<Persona>, int numTotal) ConsultarNombre(string nombre, int pageNumber, int pageSize);

    }
}
