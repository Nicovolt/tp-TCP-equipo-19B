using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class PresupuestoDetalle
    {
        public int Id { get; set; }
        public int IdPresupuesto { get; set; }
        public int IdProducto { get; set; }
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal { get; set; }
        public DateTime FechaAgregado { get; set; }
        public int AgregadoIdUsuario { get; set; }
        public Productos Producto { get; set; }
        public string NombreProducto { get; set; }
    }
}
