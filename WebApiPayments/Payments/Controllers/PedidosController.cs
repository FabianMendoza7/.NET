using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payments.DTOs;
using Payments.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPayments.Entidades;

namespace WebApiPayments.Controllers
{
    [ApiController]
    [Route("api/clientes/{clienteId:int}/pedidos")]

    public class PedidosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PedidosController(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get(int clienteId)
        {
            var existeCliente = await _context.Clientes.AnyAsync(x => x.Id == clienteId);

            if (!existeCliente)
            {
                return NotFound();
            }

            var pedidos = await _context.Pedidos.Where(x => x.ClienteId == clienteId).ToListAsync();

            return Ok(pedidos);
        }

        [HttpGet("{pedidoId:int}", Name = "ObtenerPedido")]
        public async Task<ActionResult> GetById(int clienteId, int pedidoId)
        {
            var existeCliente = await _context.Clientes.AnyAsync(x => x.Id == clienteId);

            if (!existeCliente)
            {
                return NotFound();
            }

            var existePedido = await _context.Pedidos.AnyAsync(x => x.Id == pedidoId);

            if (!existePedido)
            {
                return NotFound();
            }

            var pedidos = await _context.Pedidos.Where(x => x.ClienteId == clienteId && x.Id == pedidoId).ToListAsync();

            return Ok(pedidos);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int clienteId, PedidoCreacionDTO pedidoDTO)
        {
            var existeCliente = await _context.Clientes.AnyAsync(x => x.Id == clienteId);

            if (!existeCliente)
            {
                return NotFound();
            }

            // TODO: Validar que existan los productos del pedido.

            var pedido = _mapper.Map<Pedido>(pedidoDTO);
            pedido.ClienteId = clienteId;
            _context.Add(pedido);

            await _context.SaveChangesAsync();

            return CreatedAtRoute("ObtenerPedido", new { clienteId = pedido.ClienteId, pedidoId = pedido.Id }, pedidoDTO);
        }

        [HttpPut("{pedidoId:int}")]
        public async Task<ActionResult> Put(int clienteId, int pedidoId, PedidoCreacionDTO pedidoDTO)
        {
            var pedido = await _context.Pedidos.Include(x => x.Detalle).FirstOrDefaultAsync(x => x.ClienteId == clienteId && x.Id == pedidoId);

            if(pedido == null)
            {
                return NotFound();
            }

            pedido = _mapper.Map(pedidoDTO, pedido);
            foreach (var item in pedido.Detalle)
            {
                item.PedidoId = pedidoId;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
