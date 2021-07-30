using AutoMapper;
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
    [Route("api/productos")]
    public class ProductosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductosController(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> Get()
        {
            return await _context.Productos.ToListAsync();
        }

        [HttpGet("{id:int}", Name ="ObtenerProducto")]
        public async Task<ActionResult<Producto>> Get(int id)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(x=>x.Id == id);

            if(producto == null)
            {
                return NotFound();
            }

            return producto;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProductoCreacionDTO productoDTO)
        {
            string nombre = productoDTO.Nombre.Trim();
            var existe = await _context.Productos.AnyAsync(x => x.Nombre == nombre);

            if (existe)
            {
                return BadRequest($"Ya existe un producto con el nombre {nombre}");
            }

            var producto = _mapper.Map<Producto>(productoDTO);
            _context.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("ObtenerProducto", new { id = producto.Id }, productoDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(ProductoCreacionDTO productoDTO, int id)
        {
            var existe = await _context.Productos.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            var producto = _mapper.Map<Producto>(productoDTO);
            producto.Id = id;

            _context.Update(producto);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await _context.Productos.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            _context.Remove(new Producto() { Id = id });
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
