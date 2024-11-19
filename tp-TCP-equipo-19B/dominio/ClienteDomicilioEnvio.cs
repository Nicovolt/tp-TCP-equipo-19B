using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class ClienteDomicilioEnvio
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string Calle { get; set; }
        public string EntreCalles { get; set; }
        public int Altura { get; set; }
        public int? Piso { get; set; }
        public string Departamento { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public string CodigoPostal { get; set; }
        public string Observaciones { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
