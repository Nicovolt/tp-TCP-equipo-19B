using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
public class Productos
{
    public int Id_producto { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public decimal Precio { get; set; } // Cambié a decimal
    public int PorsentajeDescuento { get; set; }
    public int Id_marca { get; set; }
    public int Id_categoria { get; set; }
    public List<Imagen> ListaImagenes { get; set; }
}




}

