using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Payments.DTOs;
using Payments.Entidades;
using System;

namespace Payments.Repositorio.Pagos
{
    public class PagosRepository : IPagosRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PagosRepository(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task ActualizarEstadoPedido(Pedido pedido, string estado)
        {
            pedido.Estado = estado;
            _context.Pedidos.Attach(pedido);
            _context.Entry(pedido).Property(x => x.Estado).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cliente>> ObtenerClientes()
        {
            return await _context.Clientes.Include(x => x.Pedidos).ToListAsync();
        }

        public async Task<Cliente> ObtenerClientePorId(int id, bool incluyePedidos)
        {
            if (incluyePedidos)
            {
                return await _context.Clientes.Include(x => x.Pedidos).FirstOrDefaultAsync(x => x.Id == id);

            }
            else
            {
                return await _context.Clientes.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<List<Cliente>> ObtenerClientePorNombre(string nombres)
        {
            return await _context.Clientes.Include(x => x.Pedidos).Where(x => x.Nombres.Contains(nombres)).ToListAsync();
        }
        public async Task<Cliente> CrearCliente(ClienteCreacionDTO clienteDTO)
        {
            var cliente = _mapper.Map<Cliente>(clienteDTO);

            _context.Add(cliente);
            await _context.SaveChangesAsync();

            return cliente;
        }
        public async Task<Cliente> ActualizarCliente(ClienteCreacionDTO clienteDTO, int id)
        {
            var existe = await _context.Clientes.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return null;
            }

            var cliente = _mapper.Map<Cliente>(clienteDTO);
            cliente.Id = id;

            _context.Update(cliente);
            await _context.SaveChangesAsync();

            return cliente;
        }
        public ClientePatchDTO MapearPatchCliente(JsonPatchDocument<ClientePatchDTO> patchDocument, Cliente cliente, int id)
        {
            var clienteDTO = _mapper.Map<ClientePatchDTO>(cliente);

            return clienteDTO;
        }

        public async Task ActualizarClienteParcialmente(ClientePatchDTO clienteDTO, Cliente cliente)
        {
            _mapper.Map(clienteDTO, cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> BorrarCliente(int id)
        {
            var existe = await _context.Clientes.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return false;
            }

            _context.Remove(new Cliente() { Id = id });
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Pedido>> ObtenerPedidos(int clienteId)
        {
            var existeCliente = await _context.Clientes.AnyAsync(x => x.Id == clienteId);

            if (!existeCliente)
            {
                return null;
            }

            return await _context.Pedidos.Where(x => x.ClienteId == clienteId).ToListAsync();
        }

        public async Task<Pedido> ObtenerPedidoPorId(int clienteId, int pedidoId)
        {
            var existeCliente = await _context.Clientes.AnyAsync(x => x.Id == clienteId);

            if (!existeCliente)
            {
                return null;
            }

            var existePedido = await _context.Pedidos.AnyAsync(x => x.Id == pedidoId);

            if (!existePedido)
            {
                return null;
            }

            return await _context.Pedidos.Include(x => x.Detalle).Where(x => x.ClienteId == clienteId && x.Id == pedidoId).FirstOrDefaultAsync();
        }

        public async Task<Pedido> CrearPedido(int clienteId, PedidoCreacionDTO pedidoDTO)
        {
            var existeCliente = await _context.Clientes.AnyAsync(x => x.Id == clienteId);

            if (!existeCliente)
            {
                throw new Exception("El cliente no existe");
            }

            // TODO: Validar que existan los productos del pedido.

            var pedido = _mapper.Map<Pedido>(pedidoDTO);
            pedido.ClienteId = clienteId;
            _context.Add(pedido);

            await _context.SaveChangesAsync();

            return pedido;
        }

        public async Task<Pedido> ActualizarPedido(int clienteId, int pedidoId, PedidoCreacionDTO pedidoDTO)
        {
            var pedido = await _context.Pedidos.Include(x => x.Detalle).FirstOrDefaultAsync(x => x.ClienteId == clienteId && x.Id == pedidoId);

            if (pedido == null)
            {
                return null;
            }

            pedido = _mapper.Map(pedidoDTO, pedido);
            foreach (var item in pedido.Detalle)
            {
                item.PedidoId = pedidoId;
            }

            await _context.SaveChangesAsync();

            return pedido;
        }

        public async Task<List<Producto>> ObtenerProductos()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Producto> ObtenerProductosPorId(int id)
        {
            return await _context.Productos.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Producto> CrearProducto(ProductoCreacionDTO productoDTO)
        {
            string nombre = productoDTO.Nombre.Trim();
            var existe = await _context.Productos.AnyAsync(x => x.Nombre == nombre);

            if (existe)
            {
                throw new Exception($"Ya existe un producto con el nombre {nombre}");
            }

            var producto = _mapper.Map<Producto>(productoDTO);
            _context.Add(producto);
            await _context.SaveChangesAsync();

            return producto;
        }

        public async Task<Producto> ActualizarProducto(ProductoCreacionDTO productoDTO, int id)
        {
            var existe = await _context.Productos.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return null;
            }

            var producto = _mapper.Map<Producto>(productoDTO);
            producto.Id = id;

            _context.Update(producto);
            await _context.SaveChangesAsync();

            return producto;
        }

        public async Task<bool> BorrarProducto(int id)
        {
            var existe = await _context.Productos.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return false;
            }

            _context.Remove(new Producto() { Id = id });
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
