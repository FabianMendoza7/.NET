using AutoMapper;
using Payments.DTOs;
using Payments.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiPayments.Entidades;

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
