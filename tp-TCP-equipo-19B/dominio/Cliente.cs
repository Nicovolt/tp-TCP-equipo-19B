using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Cliente
    {
        public int Id_cliente { get; set; }
        public int Id_producto { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Gmail { get; set; }
        public string Telefono { get; set; } 

        public bool rol { get; set; }
    }
}
