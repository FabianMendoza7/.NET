using Microsoft.AspNetCore.JsonPatch;
using Payments.DTOs;
using Payments.Entidades;
using Payments.Repositorio.Pagos;
using Payments.Servicios.Facturacion;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payments.Servicios.Pagos
{
    public class PagosService : IPagosService
    {
        private readonly IPagosRepository _pagosRepository;
        private readonly IFacturacionService _facturacionService;
        private readonly ILogisticaService _logisticaService;

        public PagosService(IPagosRepository pagosRepository, IFacturacionService facturacionService, ILogisticaService logisticaService)
        {
            this._pagosRepository = pagosRepository;
            this._facturacionService = facturacionService;
            this._logisticaService = logisticaService;
        }

        public async Task<Factura> PagarPedido(int clienteId, int pedidoId)
        {
            var pedido = await ObtenerPedidoPorId(clienteId, pedidoId);

            if (pedido == null)
            {
                return null;
            }

            if (pedido.Estado == "PAGADO")
            {
                throw new Exception("El pedido ya se encuentra pagado.");
            }

            // Facturar el pedido.
            var factura = await _facturacionService.FacturarPedido(pedido);

            if (factura != null)
            {
                // Enviar el pedido (logística).
                await _logisticaService.EnviarPedido(factura);

                // Guardar el estado del pedido.
                await _pagosRepository.ActualizarEstadoPedido(pedido, "PAGADO");
            }
            else
            {
                throw new Exception("No pudo facturarse el pedido.");
            }

            return factura;
        }

        public async Task<List<Cliente>> ObtenerClientes()
        {
            return await _pagosRepository.ObtenerClientes();
        }

        public async Task<Cliente> ObtenerClientePorId(int id, bool incluyePedidos)
        {
            return await _pagosRepository.ObtenerClientePorId(id, incluyePedidos);
        }

        public async Task<List<Cliente>> ObtenerClientePorNombre(string nombres)
        {
            return await _pagosRepository.ObtenerClientePorNombre(nombres);
        }
        public async Task<Cliente> CrearCliente(ClienteCreacionDTO clienteDTO)
        {
            return await _pagosRepository.CrearCliente(clienteDTO);
        }
        public async Task<Cliente> ActualizarCliente(ClienteCreacionDTO clienteDTO, int id)
        {
            return await _pagosRepository.ActualizarCliente(clienteDTO, id);
        }
        public ClientePatchDTO MapearPatchCliente(JsonPatchDocument<ClientePatchDTO> patchDocument, Cliente cliente, int id)
        {
            return _pagosRepository.MapearPatchCliente(patchDocument, cliente, id);
        }
        public async Task ActualizarClienteParcialmente(ClientePatchDTO clienteDTO, Cliente cliente)
        {
            await _pagosRepository.ActualizarClienteParcialmente(clienteDTO, cliente);
        }
        public async Task<bool> BorrarCliente(int id)
        {
            return await _pagosRepository.BorrarCliente(id);
        }

        public async Task<List<Pedido>> ObtenerPedidos(int clienteId)
        {
            return await _pagosRepository.ObtenerPedidos(clienteId);
        }

        public async Task<Pedido> ObtenerPedidoPorId(int clienteId, int pedidoId)
        {
            return await _pagosRepository.ObtenerPedidoPorId(clienteId, pedidoId);
        }

        public async Task<Pedido> CrearPedido(int clienteId, PedidoCreacionDTO pedidoDTO)
        {
            // TODO: Validar stock para evitar la creación del pedido por falta de existencias.
            return await _pagosRepository.CrearPedido(clienteId, pedidoDTO);
        }

        public async Task<Pedido> ActualizarPedido(int clienteId, int pedidoId, PedidoCreacionDTO pedidoDTO)
        {
            return await _pagosRepository.ActualizarPedido(clienteId, pedidoId, pedidoDTO);
        }

        public async Task<List<Producto>> ObtenerProductos()
        {
            return await _pagosRepository.ObtenerProductos();
        }

        public async Task<Producto> ObtenerProductosPorId(int id)
        {
            return await _pagosRepository.ObtenerProductosPorId(id);
        }

        public async Task<Producto> CrearProducto(ProductoCreacionDTO productoDTO)
        {
            return await _pagosRepository.CrearProducto(productoDTO);
        }

        public async Task<Producto> ActualizarProducto(ProductoCreacionDTO productoDTO, int id)
        {
            return await _pagosRepository.ActualizarProducto(productoDTO, id);
        }

        public async Task<bool> BorrarProducto(int id)
        {
            return await _pagosRepository.BorrarProducto(id);
        }
    }
}
