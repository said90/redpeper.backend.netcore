using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repository
{
    public class PersonaRepository :IPersonaRepository
    {
        private DataContext _dataContext;

        public PersonaRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Persona>> GetPersonas()
        {
            return await _dataContext.Personas.ToListAsync();
        }

        public async Task<Persona> GetPersonaById(int personaId)
        {
            return await _dataContext.Personas.FirstOrDefaultAsync(x=>x.Id== personaId);
        }

        public async Task<int> GetIdPersonaMax()
        {
            return await _dataContext.Personas.Select(x => x.Id).MaxAsync();
        }

        public async Task<Persona> CrearPersona(Persona persona)
        {
            _dataContext.Personas.Add(persona);
            await _dataContext.SaveChangesAsync();
            return persona;
        }
        public async Task<Persona> ModificarPersona(Persona persona)
        {
            _dataContext.Personas.Update(persona);
            await _dataContext.SaveChangesAsync();
            return persona;
        }

        public async Task<Persona> EliminarPersona(int personaId)
        {
            var persona = await _dataContext.Personas.FirstOrDefaultAsync(x => x.Id == personaId);
            _dataContext.Personas.Remove(persona);
            await _dataContext.SaveChangesAsync();
            return persona;
        }
    }
}
