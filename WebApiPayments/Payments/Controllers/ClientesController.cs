using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payments.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPayments.Entidades;

namespace WebApiPayments.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClientesController(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> Get()
        {
            return await _context.Clientes.Include(x => x.Pedidos).ToListAsync();
        }

        [HttpGet("{id:int}", Name = "ObtenerCliente")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            var cliente = await _context.Clientes.Include(x => x.Pedidos).FirstOrDefaultAsync(x => x.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpGet("{nombres}")]
        public async Task<ActionResult<List<Cliente>>> Get([FromRoute] string nombres)
        {
            var clientes = await _context.Clientes.Include(x => x.Pedidos).Where(x => x.Nombres.Contains(nombres)).ToListAsync();

            return clientes;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ClienteCreacionDTO clienteDTO)
        {
            var cliente = _mapper.Map<Cliente>(clienteDTO);

            _context.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("ObtenerCliente", new { id = cliente.Id }, clienteDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ClienteCreacionDTO clienteDTO, int id)
        {
            var existe = await _context.Clientes.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            var cliente = _mapper.Map<Cliente>(clienteDTO);
            cliente.Id = id;

            _context.Update(cliente);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _context.Clientes.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            _context.Remove(new Cliente() { Id = id });
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<ClientePatchDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var cliente = await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);
            
            if (cliente == null)
            {
                return NotFound();
            }

            var clienteDTO = _mapper.Map<ClientePatchDTO>(cliente);
            patchDocument.ApplyTo(clienteDTO, ModelState);
            var esValido = TryValidateModel(clienteDTO);

            if (!esValido)
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(clienteDTO, cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
