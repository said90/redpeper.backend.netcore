using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Redpeper.Data;
using Redpeper.Model;
using Redpeper.Repository;

namespace Redpeper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IPersonaRepository _personaRepository;

        public ClientesController(DataContext context, IClienteRepository clienteRepository, IPersonaRepository personaRepository)
        {
            _clienteRepository = clienteRepository;
            _personaRepository = personaRepository;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _clienteRepository.GetClientes();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _clienteRepository.GetClientById(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        // PUT: api/Clientes/5
        [HttpPut]
        public async Task<IActionResult> PutCliente( Cliente cliente)
        {
            var Cliente = await _clienteRepository.GetClientById(cliente.Id);
            if (Cliente.Equals(null))
            {
                return BadRequest();
            }

            var c= await _clienteRepository.ModificarCliente(Cliente);

            return Ok(c);
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Persona persona)
        {
            await _personaRepository.CrearPersona(persona);
            persona.Id = await _personaRepository.GetIdPersonaMax();
            Cliente cliente = new Cliente
            {
                idPersona = persona.Id,
                Persona = persona
            };
            await _clienteRepository.CrearCliente(cliente);
            return cliente;
        }

        // DELETE: api/Clientes/5
        [HttpDelete]

        public async Task<ActionResult<Cliente>> DeleteCliente(int id)
        {
            var client = await _clienteRepository.GetClientById(id);
            if (client == null)
            {
                return NotFound();
            }

            await _clienteRepository.EliminarCliente(id);

            return client;
        }

    }
}
