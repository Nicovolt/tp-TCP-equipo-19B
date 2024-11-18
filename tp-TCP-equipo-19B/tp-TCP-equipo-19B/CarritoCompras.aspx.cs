﻿using negocio;
using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_TCP_equipo_19B
{
    public partial class Formulario_web14 : System.Web.UI.Page
    {
        private ProductoNegocio ProductoNegocio = new ProductoNegocio();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CarritoCompras"] == null)
            {
                List<dominio.Productos> Newcarrito = new List<dominio.Productos>();
                Session.Add("CarritoCompras", Newcarrito);
                actualizarTotalCarrito();
                if (!IsPostBack)
                {
                    CargarCarrito();
                    if (Session["TotalCarrito"] != null)
                    {
                        lblPrecioTotal.Text = Session["TotalCarrito"].ToString();
                    }
                    else
                    {
                        actualizarTotalCarrito();
                    }
                }
            }
        }
        private void EliminarArticulo(dominio.Productos articulo)
        {
            List<dominio.Productos> carrito = new List<dominio.Productos>();
            carrito = (List<dominio.Productos>)Session["CarritoCompras"];

            for (int i = 0; i < carrito.Count; i++)
            {
                if (carrito[i].Id_producto == articulo.Id_producto)
                {
                    carrito.RemoveAt(i);
                    return;
                }
            }
        }

        private void CargarCarrito()
        {
            List<dominio.Productos> carrito = (List<dominio.Productos>)Session["CarritoCompras"];
            repeaterCarrito.DataSource = carrito;
            repeaterCarrito.DataBind();
        }

        private void actualizarTotalCarrito()
        {
            List<dominio.Productos> carrito = (List<dominio.Productos>)Session["CarritoCompras"];
            decimal totalCarrito = 0;

            foreach (dominio.Productos item in carrito)
            {
                totalCarrito += item.Cantidad * item.Precio;
            }
            Session["TotalCarrito"] = totalCarrito.ToString("0.00");

            lblPrecioTotal.Text = totalCarrito.ToString("0.00");
        }

        protected void btnAumentarCantidad_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            List<dominio.Productos> carrito = (List<dominio.Productos>)Session["CarritoCompras"];

            dominio.Productos articulo = carrito.FirstOrDefault(a => a.Id_producto == id);
            if (articulo != null)
            {
                articulo.Cantidad += 1;
            }

            CargarCarrito();
            actualizarTotalCarrito();
        }

        protected void btnDisminuirCantidad_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            List<dominio.Productos> carrito = (List<dominio.Productos>)Session["CarritoCompras"];

            dominio.Productos articulo = carrito.FirstOrDefault(a => a.Id_producto == id);
            if (articulo != null && articulo.Cantidad > 1)
            {
                articulo.Cantidad -= 1;
            }

            CargarCarrito();
            actualizarTotalCarrito();
        }

        private void ActualizarContadorCarrito(int cantArticulos)
        {
        
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).CommandArgument);
            dominio.Productos articulo = new dominio.Productos();
            articulo = ProductoNegocio.buscarPorID(id);
            List<dominio.Productos> carrito = new List<dominio.Productos>();
            carrito = (List<dominio.Productos>)Session["CarritoCompras"];

            EliminarArticulo(articulo);
            CargarCarrito();
            actualizarTotalCarrito();
            ActualizarContadorCarrito(carrito.Count);

        }

        protected void btnComprar_Click(object sender, EventArgs e)
        {
            if (Session["usuario"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else 
            {
                Session["CarritoCompras"] = null;
                Response.Redirect("Compra.aspx");         
            }
        }

    }
}