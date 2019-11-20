using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Redpeper.Model;

namespace Redpeper.Repository
{
    public interface IClienteRepository
    {
        Task<List<Cliente>> GetClientes();
        Task<Cliente> GetClientById(int clienteId);
        Task<Cliente> GetClienteByPersonaId(int personaId);
        Task<Cliente> CrearCliente(Cliente cliente);
        Task<Cliente> ModificarCliente(Cliente cliente);
        Task<Cliente> EliminarCliente(int clienteId);

    }
}
