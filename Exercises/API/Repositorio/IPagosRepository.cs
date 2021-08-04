using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;
using Payments.DTOs;
using Payments.Entidades;

namespace Payments.Repositorio.Pagos
{
    public interface IPagosRepository
    {
        Task ActualizarEstadoPedido(Pedido pedido, string estado);
        Task<List<Cliente>> ObtenerClientes();
        Task<Cliente> ObtenerClientePorId(int id, bool incluyePedidos);
        Task<List<Cliente>> ObtenerClientePorNombre(string nombres);
        Task<Cliente> CrearCliente(ClienteCreacionDTO clienteDTO);
        Task<Cliente> ActualizarCliente(ClienteCreacionDTO clienteDTO, int id);
        ClientePatchDTO MapearPatchCliente(JsonPatchDocument<ClientePatchDTO> patchDocument, Cliente cliente, int id);
        Task ActualizarClienteParcialmente(ClientePatchDTO clienteDTO, Cliente cliente);
        Task<bool> BorrarCliente(int id);
        Task<List<Pedido>> ObtenerPedidos(int clienteId);
        Task<Pedido> ObtenerPedidoPorId(int clienteId, int pedidoId);
        Task<Pedido> CrearPedido(int clienteId, PedidoCreacionDTO pedidoDTO);
        Task<Pedido> ActualizarPedido(int clienteId, int pedidoId, PedidoCreacionDTO pedidoDTO);
        Task<List<Producto>> ObtenerProductos();
        Task<Producto> ObtenerProductosPorId(int id);
        Task<Producto> CrearProducto(ProductoCreacionDTO productoDTO);
        Task<Producto> ActualizarProducto(ProductoCreacionDTO productoDTO, int id);
        Task<bool> BorrarProducto(int id);
    }
}
