using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Presupuesto
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdMetodoEnvio { get; set; }
        public short IdEstado { get; set; }
        public int IdFormaPago { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaValidez { get; set; }
        public int IdClienteEnvio { get; set; }
        public decimal CostoEnvio { get; set; }
        public decimal Total { get; set; }
        public DateTime UltimaActualizacion { get; set; }
        public List<PresupuestoDetalle> Detalles { get; set; }
        public EnvioTipo MetodoEnvio { get; set; }
        public PresupuestoEstado Estado { get; set; }
        public PresupuestoFormaPago FormaPago { get; set; }
        public ClienteDomicilioEnvio DomicilioEnvio { get; set; }
        public Cliente Cliente { get; set; }
    }

}
