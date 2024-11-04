using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public int IdCliente { get; set; }
        public string Mail { get; set; }
        public byte[] Contrasena { get; set; }
    }
}
