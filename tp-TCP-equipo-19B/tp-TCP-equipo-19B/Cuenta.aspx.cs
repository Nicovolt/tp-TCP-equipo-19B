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
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!ValidarSesion())
            {
                Response.Redirect("Login.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarDatosUsuario();
                CargarDirecciones();
            }
        }

        private bool ValidarSesion()
        {
            return Session["usuario"] != null;
        }

        private void CargarDatosUsuario()
        {
            try
            {
                dynamic usuario = Session["usuario"];
                ClienteNegocio negocio = new ClienteNegocio();
                Cliente cliente = negocio.ObtenerClientePorId(usuario.IdCliente);

                if (cliente != null)
                {
                    txtNombre.Text = cliente.Nombre;
                    txtApellido.Text = cliente.Apellido;
                    txtEmail.Text = cliente.Mail;
                    txtTelefono.Text = cliente.Telefono;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar los datos: " + ex.Message, "danger");
            }
        }

        private void CargarDirecciones()
        {
            try
            {
                dynamic usuario = Session["usuario"];
                ClienteDomicilioEnvioNegocio negocio = new ClienteDomicilioEnvioNegocio();
                var direcciones = negocio.ListarPorCliente(usuario.IdCliente);
                rptDirecciones.DataSource = direcciones;
                rptDirecciones.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar las direcciones: " + ex.Message, "danger");
            }
        }

        protected void btnGuardarDatos_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                dynamic usuario = Session["usuario"];
                ClienteNegocio negocio = new ClienteNegocio();

                Cliente cliente = new Cliente
                {
                    Id_cliente = usuario.IdCliente,
                    Nombre = txtNombre.Text.Trim(),
                    Apellido = txtApellido.Text.Trim(),
                    Mail = txtEmail.Text.Trim(),
                    Telefono = txtTelefono.Text.Trim()
                };

                negocio.Modificar(cliente);
                MostrarMensaje("Datos actualizados correctamente", "success");
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al actualizar los datos: " + ex.Message, "danger");
            }
        }

        protected void btnCambiarPassword_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                dynamic usuario = Session["usuario"];
                UsuarioNegocio negocio = new UsuarioNegocio();

                // Verificar contraseña actual
                if (!negocio.ValidarPassword(usuario.IdCliente, txtPasswordActual.Text))
                {
                    MostrarMensaje("La contraseña actual es incorrecta", "danger");
                    return;
                }

                // Cambiar contraseña
                negocio.CambiarPassword(usuario.IdCliente, txtNuevaPassword.Text);

                // Limpiar campos
                txtPasswordActual.Text = string.Empty;
                txtNuevaPassword.Text = string.Empty;
                txtConfirmarPassword.Text = string.Empty;

                MostrarMensaje("Contraseña actualizada correctamente", "success");
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cambiar la contraseña: " + ex.Message, "danger");
            }
        }

        private void MostrarPanelDireccion(int idDireccion)
        {
            // Mostrar panel y cargar datos si es edición
            pnlNuevaDireccion.Visible = true;
            ViewState["IdDireccionEditar"] = idDireccion;

            if (idDireccion > 0)
            {
                try
                {
                    ClienteDomicilioEnvioNegocio negocio = new ClienteDomicilioEnvioNegocio();
                    var direccion = negocio.ObtenerPorId(idDireccion);
                    if (direccion != null)
                    {
                        // Cargar datos en los controles del panel
                        // (Implementar según los controles que agregues en el panel)
                    }
                }
                catch (Exception ex)
                {
                    MostrarMensaje("Error al cargar la dirección: " + ex.Message, "danger");
                }
            }
        }

        private void MostrarMensaje(string mensaje, string tipo)
        {
            pnlMensaje.CssClass = $"alert alert-{tipo} alert-dismissible fade show mb-4";
            lblMensaje.Text = mensaje;
            pnlMensaje.Visible = true;
        }

        protected void btnNuevaDireccion_Click(object sender, EventArgs e)
        {
            LimpiarFormularioDireccion();
            pnlNuevaDireccion.Visible = true;
            ltlTituloDireccion.Text = "Nueva Dirección";
            ViewState["IdDireccion"] = null;
            
            ScriptManager.RegisterStartupScript(this, GetType(), "activateTab",
                "document.getElementById('direcciones-tab').click();", true);
        }

        protected void btnCancelarDireccion_Click(object sender, EventArgs e)
        {
            pnlNuevaDireccion.Visible = false;
            LimpiarFormularioDireccion();
        }

        protected void btnGuardarDireccion_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                dynamic usuario = Session["usuario"];
                ClienteDomicilioEnvio domicilio = new ClienteDomicilioEnvio
                {
                    IdCliente = usuario.IdCliente,
                    Calle = txtCalle.Text.Trim(),
                    Altura = int.Parse(txtAltura.Text),
                    EntreCalles = string.IsNullOrEmpty(txtEntreCalles.Text) ? null : txtEntreCalles.Text.Trim(),
                    Piso = string.IsNullOrEmpty(txtPiso.Text) ? null : (int?)int.Parse(txtPiso.Text),
                    Departamento = string.IsNullOrEmpty(txtDepartamento.Text) ? null : txtDepartamento.Text.Trim(),
                    Localidad = txtLocalidad.Text.Trim(),
                    Provincia = ddlProvincia.SelectedValue,
                    CodigoPostal = txtCP.Text.Trim(),
                    Observaciones = string.IsNullOrEmpty(txtObservaciones.Text) ? null : txtObservaciones.Text.Trim(),
                    Activo = true
                };

                ClienteDomicilioEnvioNegocio negocio = new ClienteDomicilioEnvioNegocio();

                // Verificar si es edición o nueva dirección
                if (ViewState["IdDireccion"] != null)
                {
                    domicilio.Id = (int)ViewState["IdDireccion"];
                    negocio.Modificar(domicilio);
                    MostrarMensaje("Dirección actualizada correctamente", "success");
                }
                else
                {
                    negocio.Agregar(domicilio);
                    MostrarMensaje("Dirección agregada correctamente", "success");
                }

                // Ocultar formulario y recargar lista
                pnlNuevaDireccion.Visible = false;
                CargarDirecciones();
                LimpiarFormularioDireccion();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al guardar la dirección: " + ex.Message, "danger");
            }
        }

        protected void rptDirecciones_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            int idDireccion = Convert.ToInt32(e.CommandArgument);
            ClienteDomicilioEnvioNegocio negocio = new ClienteDomicilioEnvioNegocio();

            try
            {
                switch (e.CommandName)
                {
                    case "Editar":
                        var direccion = negocio.ObtenerPorId(idDireccion);
                        if (direccion != null)
                        {
                            CargarDireccionParaEditar(direccion);
                        }
                        break;

                    case "Eliminar":
                        negocio.Eliminar(idDireccion);
                        CargarDirecciones();
                        MostrarMensaje("Dirección eliminada correctamente", "success");
                        break;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, "danger");
            }
        }

        private void CargarDireccionParaEditar(ClienteDomicilioEnvio direccion)
        {
            ViewState["IdDireccion"] = direccion.Id;
            ltlTituloDireccion.Text = "Editar Dirección";

            txtCalle.Text = direccion.Calle;
            txtAltura.Text = direccion.Altura.ToString();
            txtEntreCalles.Text = direccion.EntreCalles;
            txtPiso.Text = direccion.Piso?.ToString();
            txtDepartamento.Text = direccion.Departamento;
            txtLocalidad.Text = direccion.Localidad;
            ddlProvincia.SelectedValue = direccion.Provincia;
            txtCP.Text = direccion.CodigoPostal;
            txtObservaciones.Text = direccion.Observaciones;

            pnlNuevaDireccion.Visible = true;

            // Mantener activa la pestaña de direcciones
            ScriptManager.RegisterStartupScript(this, GetType(), "activateTab",
                "document.getElementById('direcciones-tab').click();", true);
        }

        private void LimpiarFormularioDireccion()
        {
            txtCalle.Text = string.Empty;
            txtAltura.Text = string.Empty;
            txtEntreCalles.Text = string.Empty;
            txtPiso.Text = string.Empty;
            txtDepartamento.Text = string.Empty;
            txtLocalidad.Text = string.Empty;
            ddlProvincia.SelectedIndex = 0;
            txtCP.Text = string.Empty;
            txtObservaciones.Text = string.Empty;
            ViewState["IdDireccion"] = null;
        }
    }
}