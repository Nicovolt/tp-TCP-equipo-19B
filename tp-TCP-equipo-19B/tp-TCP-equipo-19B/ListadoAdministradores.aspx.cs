using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tp_TCP_equipo_19B
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar si el usuario actual es administrador
                if (!EsUsuarioAdmin())
                {
                    Response.Redirect("Default.aspx");
                    return;
                }

                CargarUsuarios();
            }
        }

        private bool EsUsuarioAdmin()
        {
            if (Session["usuario"] == null) return false;
            dynamic usuario = Session["usuario"];
            if (usuario.EsAdmin)
            {
                return true;
            }
            return false;
        }

        private void CargarUsuarios(string filtro = "todos", string busqueda = "")
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                List<UsuarioDetalle> usuarios = negocio.listarUsuariosDetalle();

                // Aplicar filtro de admin/no admin
                if (filtro != "todos")
                {
                    bool esAdmin;
                    if (filtro == "true")
                    {
                        esAdmin = true;
                    }
                    else
                    {
                        esAdmin = false;
                    }
                    usuarios = usuarios.Where(u => u.Admin == esAdmin).ToList();
                }

                // Aplicar búsqueda si existe
                if (!string.IsNullOrEmpty(busqueda))
                {
                    usuarios = usuarios.Where(u =>
                        u.NombreCompleto.ToLower().Contains(busqueda.ToLower()) ||
                        u.Mail.ToLower().Contains(busqueda.ToLower())
                    ).ToList();
                }

                gvUsuarios.DataSource = usuarios;
                gvUsuarios.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar usuarios: " + ex.Message, "danger");
            }
        }

        private void MostrarMensaje(string mensaje, string tipo)
        {
            pnlMensaje.CssClass = $"alert alert-{tipo} alert-dismissible fade show mt-3";
            lblMensaje.Text = mensaje;
            pnlMensaje.Visible = true;
        }

        protected void ddlFiltroAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarUsuarios(ddlFiltroAdmin.SelectedValue, txtBuscar.Text);
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarUsuarios(ddlFiltroAdmin.SelectedValue, txtBuscar.Text);
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = string.Empty;
            ddlFiltroAdmin.SelectedValue = "todos";
            CargarUsuarios();
        }

        protected void chkAdmin_CheckedChanged(object sender, EventArgs e)
        {
            GridViewRow row = ((CheckBox)sender).NamingContainer as GridViewRow;
            Button btnGuardar = (Button)row.FindControl("btnGuardar");
            if(btnGuardar.Visible == true)
            {
                btnGuardar.Visible = false;
            }
            else
            {
                btnGuardar.Visible = true;
            }
        }

        protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "GuardarCambios")
            {
                int idUsuario = Convert.ToInt32(e.CommandArgument);
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvUsuarios.Rows.Cast<GridViewRow>().FirstOrDefault(r => Convert.ToInt32(gvUsuarios.DataKeys[r.RowIndex].Value) == idUsuario);
                // Con esta linea seleccionamos, del listado, la linea que seleccionamos en el guardar, para poder actualizar el estado del administador. 

                dynamic usuario = Session["usuario"];

                if (row != null)
                {
                    CheckBox chkAdmin = (CheckBox)row.FindControl("chkAdmin");
                    try
                    {
                        UsuarioNegocio negocio = new UsuarioNegocio();
                        if (usuario.EsAdmin == false)
                        {
                            MostrarMensaje("Usted no cuenta con permisos de administrador, por favor actualice la pagina", "danger");
                            return;
                        }
                        negocio.ActualizarEstadoAdmin(idUsuario, chkAdmin.Checked);
                        actualizarSesion(idUsuario, chkAdmin.Checked);
                        Button btnGuardar = (Button)row.FindControl("btnGuardar");
                        btnGuardar.Visible = false;
                        MostrarMensaje("Cambios guardados correctamente", "success");
                    }
                    catch (Exception ex)
                    {
                        MostrarMensaje("Error al guardar cambios: " + ex.Message, "danger");
                    }
                }
            }
        }

        protected void gvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Aquí puedes personalizar la apariencia de cada fila
                // Por ejemplo, resaltar usuarios administradores
                if ((bool)DataBinder.Eval(e.Row.DataItem, "Admin"))
                {
                    e.Row.CssClass += " table-primary";
                }
            }
        }

        private void actualizarSesion(int idUsuario, bool admin)
        {
            dynamic usuarioActual = Session["usuario"];
            if (usuarioActual.IdUsuario == idUsuario)
            {
                var sesionUsuario = new
                {
                    IdUsuario = usuarioActual.IdUsuario,
                    IdCliente = usuarioActual.IdCliente,
                    Mail = usuarioActual.Mail,
                    FechaLogin = usuarioActual.FechaLogin,
                    EsAdmin = admin
                };
                Session["usuario"] = sesionUsuario;
            }
        }
    }
}