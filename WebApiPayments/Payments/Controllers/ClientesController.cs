using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPayments.Entidades;

namespace WebApiPayments.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> Get()
        {
            return await _context.Clientes.Include(x=>x.Pedidos).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Cliente cliente)
        {
            _context.Add(cliente);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Cliente cliente, int id)
        {
            if(cliente.Id != id)
            {
                return BadRequest("El id del cliente no coincide con el id de la URL");
            }

            var existe = await _context.Clientes.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            _context.Update(cliente);
            await _context.SaveChangesAsync();
            return Ok();
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
            return Ok();
        }
    }
}
