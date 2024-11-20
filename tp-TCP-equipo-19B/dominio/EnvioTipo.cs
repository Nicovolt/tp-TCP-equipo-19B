using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class EnvioTipo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string UrlImagen { get; set; }
        public decimal Costo { get; set; }
        public bool Activo { get; set; }
    }
}
