using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class UsuarioDetalle
    {
        public int IdUsuario { get; set; }
        public int IdCliente { get; set; }
        public string NombreCompleto { get; set; }
        public string Mail { get; set; }
        public bool Admin { get; set; }
    }
}
