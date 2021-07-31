using Payments.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payments.Repositorio.Logistica
{
    public interface ILogisticaRepository
    {
        Task<Envio> CrearEnvio(Factura factura);
        Task ActualizarEstadoEnvio(Envio envio);
    }
}
