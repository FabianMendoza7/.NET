using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payments.DTOs;
using Payments.Entidades;
using Payments.Servicios.Pagos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payments.Controllers
{
    [ApiController]
    [Route("api/productos")]
    public class ProductosController : ControllerBase
    {
        private readonly IPagosService _pagosService;

        public ProductosController(IPagosService pagosService)
        {
            this._pagosService = pagosService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> Get()
        {
            return await _pagosService.ObtenerProductos();
        }

        [HttpGet("{id:int}", Name = "ObtenerProducto")]
        public async Task<ActionResult<Producto>> Get(int id)
        {
            var producto = await _pagosService.ObtenerProductosPorId(id);

            if (producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProductoCreacionDTO productoDTO)
        {
            var producto = new Producto();

            try
            {
                producto = await _pagosService.CrearProducto(productoDTO);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }

            return CreatedAtRoute("ObtenerProducto", new { id = producto.Id }, productoDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ProductoCreacionDTO productoDTO, int id)
        {
            var producto = await _pagosService.ActualizarProducto(productoDTO, id);

            if (producto == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            bool borrado = await _pagosService.BorrarProducto(id);

            if (!borrado)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
