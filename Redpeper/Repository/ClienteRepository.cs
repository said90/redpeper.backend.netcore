using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;

namespace Redpeper.Repository
{
    public class ClienteRepository :IClienteRepository
    {
        private DataContext _dataContext;

        public ClienteRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Cliente>> GetClientes()
        {
            return await _dataContext.Clientes.Include(x=>x.Persona).ToListAsync();
        }

        public async Task<Cliente> GetClientById(int clienteId)
        {
            return await _dataContext.Clientes.FirstOrDefaultAsync(x => x.Id == clienteId);
        }

        public async Task<Cliente> GetClienteByPersonaId(int personaId)
        {
            return await _dataContext.Clientes.FirstOrDefaultAsync(x => x.idPersona == personaId);
        }

        public async Task<Cliente> CrearCliente(Cliente cliente)
        {
            
            _dataContext.Clientes.Add(cliente);
            await _dataContext.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> ModificarCliente(Cliente cliente)
        {
            _dataContext.Clientes.Update(cliente);
            await _dataContext.SaveChangesAsync();
            return cliente;
        }

        public async Task<Cliente> EliminarCliente(int clienteId)
        {
            var cliente=  await _dataContext.Clientes.FirstOrDefaultAsync(x=> x.Id==clienteId);
            _dataContext.Clientes.Remove(cliente);
            await _dataContext.SaveChangesAsync();
            return cliente;
        }


    }
}
