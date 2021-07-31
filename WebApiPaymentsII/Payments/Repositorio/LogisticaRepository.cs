using Payments.Entidades;
using System;
using System.Threading.Tasks;

namespace Payments.Repositorio.Logistica
{
    public class LogisticaRepository: ILogisticaRepository
    {
        private readonly ApplicationDbContext _context;

        public LogisticaRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public async Task<Envio> CrearEnvio(Factura factura)
        {
            Envio envio = new Envio()
            {
                FacturaId = factura.Id,
                Estado = "POR_ENVIAR",
                FechaEstado = DateTime.Now
            };

            await ActualizarEstadoEnvio(envio);

            return envio;
        }
                
        public async Task ActualizarEstadoEnvio(Envio envio)
        {
            // Cada registro de envío corresponde al seguimiento de una factura.
            _context.Add(envio);
            await _context.SaveChangesAsync();
        }
    }
}
