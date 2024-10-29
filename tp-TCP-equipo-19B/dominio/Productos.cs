using dominio;
using System.Collections.Generic;

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
