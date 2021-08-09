using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Payments.DTOs;
using Payments.Entidades;
using Payments.Servicios.Facturacion;
using Payments.Servicios.Pagos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments.Controllers
{
    [ApiController]
    [Route("api/clientes/{clienteId:int}/pedidos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PedidosController : ControllerBase
    {
        private readonly IPagosService _pagosService;
        private readonly IFacturacionService _facturaService;
        private readonly UserManager<IdentityUser> _userManager;

        public PedidosController(IPagosService pagosService, 
                                 IFacturacionService facturaService, 
                                 UserManager<IdentityUser> userManager)
        {
            this._pagosService = pagosService;
            this._facturaService = facturaService;
            this._userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> Get(int clienteId)
        {
            var pedidos = await _pagosService.ObtenerPedidos(clienteId);

            if (pedidos == null)
            {
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
            // Si quisiéramos conocer el valor de un determinado claim para usarlo (en este caso el Id del usuario).
            // Nota: se debe eliminar el mapeo por defecto de los claims en Startup.cs.
            //       se debe contar con una propiedad de IdentityUser en la entidad Pedido.cs.
            //var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            //var email = emailClaim.Value;
            //var usuario = await _userManager.FindByEmailAsync(email);
            //var usuarioId = await usuario.Id; //Esta variable la podemos asignar a la entidad a actualizar (linea 72)

            try
            {
                var pedido = await _pagosService.CrearPedido(clienteId, pedidoDTO);
                //pedido.UsuarioId = usuarioId;

                return CreatedAtRoute("ObtenerPedido", new { clienteId = clienteId, pedidoId = pedido.Id }, pedidoDTO);

            }
            catch (Exception error)
            {
                return NotFound(error.Message);
            }
        }

        [HttpPost("{pedidoId:int}")]
        public async Task<ActionResult<Factura>> Pay(int clienteId, int pedidoId)
        {
            try
            {
                var factura = await _pagosService.PagarPedido(clienteId, pedidoId);

                if (factura == null)
                {
                    return NotFound();
                }

                // TODO: fix route:
                // return CreatedAtRoute("ObtenerFactura", new { clienteId, pedidoId }, factura);
                return factura;
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpPut("{pedidoId:int}")]
        public async Task<ActionResult> Put(int clienteId, int pedidoId, PedidoCreacionDTO pedidoDTO)
        {
            var pedido = await _pagosService.ActualizarPedido(clienteId, pedidoId, pedidoDTO);

            if (pedido == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // TODO: Fix route.
        /*
        [HttpGet("{pedidoId:int}", Name = "ObtenerFactura")]
        public async Task<ActionResult<Factura>> GetBilling(int clienteId, int pedidoId)
        {
            var factura = await _facturaService.ObtenerFacturaPorPedidoId(pedidoId);

            if (factura == null)
            {
                return NotFound();
            }

            return factura;
        }
        */
    }
}
