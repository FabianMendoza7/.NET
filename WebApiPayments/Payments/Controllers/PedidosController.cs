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
    [Route("api/pedidos")]

    public class PedidosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PedidosController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pedido>> Get(int id)
        {
            return await _context.Pedidos.Include(x => x.Cliente).FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Pedido pedido)
        {
            var existeCliente = await _context.Clientes.AnyAsync(x => x.Id == pedido.ClienteId);

            if (!existeCliente)
            {
                return BadRequest($"No existe el cliente de Id: {pedido.ClienteId}");
            }

            _context.Add(pedido);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
