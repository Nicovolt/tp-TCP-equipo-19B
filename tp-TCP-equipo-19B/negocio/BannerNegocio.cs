using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace negocio
{
    public class BannerNegocio
    {
        AccesoDatos data = new AccesoDatos();

        public void listar()
        {
            data.setearConsulta("SELECT * from Home_banner");
            data.ejecutarLectura();
        }


        public void Agregar(string url, int user)
        {
            data.setearConsulta("insert  into Home_banner (url_banner,activo,id_cuenta) values (@url,1,@id)");
            data.setearParametro("@url", url);
            data.setearParametro("@id", user);
            data.ejecutarLectura();
        }


    }
}
