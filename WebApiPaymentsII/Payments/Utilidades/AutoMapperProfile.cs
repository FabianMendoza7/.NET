using AutoMapper;
using Payments.DTOs;
using Payments.Entidades;

namespace Payments.Utilidades
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ClienteCreacionDTO, Cliente>();
            CreateMap<ClientePatchDTO, Cliente>().ReverseMap();
            CreateMap<ProductoCreacionDTO, Producto>();
            CreateMap<PedidoCreacionDTO, Pedido>();
            CreateMap<PedidoDetalleCreacionDTO, PedidoDetalle>();
        }
    }
}
