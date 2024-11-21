using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Banner
    {
        public int IdBanner { get; set; }
        public string UrlBanner { get; set; }
        public bool Activo { get; set; }
       // public datetime fecha { get; set; }
        public int Orden { get; set; }

    }
}
