using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Payments.DTOs;
using Payments.Entidades;
using Payments.Servicios.Pagos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payments.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdmin")]
    public class ClientesController : ControllerBase
    {
        private readonly IPagosService _pagosService;

        public ClientesController(IPagosService pagosService)
        {
            this._pagosService = pagosService;
        }

        //private void GenerarEnlaces(ClienteCreacionDTO clienteDTO)
        //{
        //    clienteDTO.Enlaces.Add(new DatoHATEOAS(
        //        enlace: Url.Link("obtenerCliente", new { id = clienteDTO.Id }),
        //        descripcion: "self",
        //        metodo: "GET"));

        //    clienteDTO.Enlaces.Add(new DatoHATEOAS(
        //        enlace: Url.Link("actualizarCliente", new { id = clienteDTO.Id }),
        //        descripcion: "cliente-actualizar",
        //        metodo: "PUT"));

        //    clienteDTO.Enlaces.Add(new DatoHATEOAS(
        //        enlace: Url.Link("borrarCliente", new { id = clienteDTO.Id }),
        //        descripcion: "cliente-borrar",
        //        metodo: "DELETE"));
        //}

        [HttpGet(Name = "obtenerClientes")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Cliente>>> Get()
        {
            return await _pagosService.ObtenerClientes();
        }

        [HttpGet("{id:int}", Name = "obtenerCliente")]
        public async Task<ActionResult<Cliente>> Get(int id)
        {
            var cliente = await _pagosService.ObtenerClientePorId(id, true);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        [HttpGet("{nombres}", Name = "obtenerClientePorNombre")]
        public async Task<ActionResult<List<Cliente>>> Get([FromRoute] string nombres)
        {
            var clientes = await _pagosService.ObtenerClientePorNombre(nombres);

            return clientes;
        }

        [HttpPost(Name = "crearCliente")]
        public async Task<ActionResult> Post(ClienteCreacionDTO clienteDTO)
        {
            var cliente = await _pagosService.CrearCliente(clienteDTO);

            return CreatedAtRoute("obtenerCliente", new { id = cliente.Id }, clienteDTO);
        }

        [HttpPut("{id:int}", Name = "actualizarCliente")]
        public async Task<ActionResult> Put(ClienteCreacionDTO clienteDTO, int id)
        {

            var cliente = await _pagosService.ActualizarCliente(clienteDTO, id);

            if (cliente == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "actualizarClienteParcialmente")]
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

        [HttpDelete("{id:int}", Name = "borrarCliente")]
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
