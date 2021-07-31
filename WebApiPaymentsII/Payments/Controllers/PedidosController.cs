using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Payments.Servicios.Pagos;
using Payments.DTOs;
using Payments.Entidades;

namespace Payments.Controllers
{
    [ApiController]
    [Route("api/clientes/{clienteId:int}/pedidos")]

    public class PedidosController : ControllerBase
    {
        private readonly IPagosService _pagosService;

        public PedidosController(IPagosService pagosService)
        {
            this._pagosService = pagosService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> Get(int clienteId)
        {
            var pedidos = await _pagosService.ObtenerPedidos(clienteId);

            if (pedidos == null) {
                return NotFound();
            }

            return Ok(pedidos);
        }

        [HttpGet("{pedidoId:int}", Name = "ObtenerPedido")]
        public async Task<ActionResult<Pedido>> GetById(int clienteId, int pedidoId)
        {
            var pedido = await _pagosService.ObtenerPedidoPorId(clienteId, pedidoId);

            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        [HttpPost]
        public async Task<ActionResult> Post(int clienteId, PedidoCreacionDTO pedidoDTO)
        {
            var pedido = await _pagosService.CrearPedido(clienteId, pedidoDTO);

            if (pedido == null)
            {
                return NotFound();
            }

            return CreatedAtRoute("ObtenerPedido", new { clienteId = clienteId, pedidoId = pedido.Id }, pedidoDTO);
        }

        [HttpPost("{pedidoId:int}")]
        public async Task<ActionResult<Pedido>> Pay(int clienteId, int pedidoId)
        {
            var pedido = await _pagosService.PagarPedido(clienteId, pedidoId);

            if (pedido == null)
            {
                return NotFound();
            }

            if(pedido.Estado == "PAGADO")
            {
                return BadRequest("El pedido ya se encuentra pagado.");
            }

            return CreatedAtRoute("ObtenerPedido", new { clienteId = clienteId, pedidoId = pedido.Id }, pedido);
        }

        [HttpPut("{pedidoId:int}")]
        public async Task<ActionResult> Put(int clienteId, int pedidoId, PedidoCreacionDTO pedidoDTO)
        {
            var pedido = await _pagosService.ActualizarPedido(clienteId, pedidoId, pedidoDTO);

            if(pedido == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
