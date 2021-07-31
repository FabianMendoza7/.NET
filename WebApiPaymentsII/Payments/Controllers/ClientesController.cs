using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Payments.DTOs;
using Payments.Entidades;
using Payments.Servicios.Pagos;
using System;

namespace Payments.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClientesController : ControllerBase
    {
        private readonly IPagosService _pagosService;

        public ClientesController(IPagosService pagosService)
        {
            this._pagosService = pagosService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> Get()
        {
            return await _pagosService.ObtenerClientes();
        }

        [HttpGet("{id:int}", Name = "ObtenerCliente")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            var cliente = await _pagosService.ObtenerClientePorId(id, true);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpGet("{nombres}")]
        public async Task<ActionResult<List<Cliente>>> Get([FromRoute] string nombres)
        {
            var clientes = await _pagosService.ObtenerClientePorNombre(nombres);

            return clientes;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ClienteCreacionDTO clienteDTO)
        {
            var cliente = await _pagosService.CrearCliente(clienteDTO);

            return CreatedAtRoute("ObtenerCliente", new { id = cliente.Id }, clienteDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ClienteCreacionDTO clienteDTO, int id)
        {

            var cliente = await _pagosService.ActualizarCliente(clienteDTO, id);

            if (cliente == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<ClientePatchDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var cliente = await _pagosService.ObtenerClientePorId(id, false);

            if (cliente == null)
            {
                return NotFound();
            }

            var clienteDTO = _pagosService.MapearPatchCliente(patchDocument, cliente, id);
            patchDocument.ApplyTo(clienteDTO, ModelState);
            bool esValido = TryValidateModel(clienteDTO);

            if (!esValido)
            {
                return BadRequest(ModelState);
            }

            await _pagosService.ActualizarClienteParcialmente(clienteDTO, cliente);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool borrado = await _pagosService.BorrarCliente(id);

            if (!borrado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
