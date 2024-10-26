using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Imagen
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public string UrlImagen { get; set; }
        public bool Activo { get; set; }
    }
}
