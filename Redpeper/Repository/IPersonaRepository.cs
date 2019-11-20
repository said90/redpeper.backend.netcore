using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repository
{
    public interface IPersonaRepository
    {
        Task<List<Persona>> GetPersonas();
        Task<Persona> GetPersonaById(int personaId);
        Task<int> GetIdPersonaMax();
        Task<Persona> CrearPersona(Persona persona);
        Task<Persona> ModificarPersona(Persona persona);
        Task<Persona> EliminarPersona(int personaId);
    }
}
