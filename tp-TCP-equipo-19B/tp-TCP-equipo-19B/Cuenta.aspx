<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cuenta.aspx.cs" Inherits="tp_TCP_equipo_19B.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container py-5">
        <div class="row justify-content-center">
            <div class="col-md-10">
                <div class="card shadow">
                    <div class="card-header bg-primary text-white py-3">
                        <h3 class="mb-0">Mi Cuenta</h3>
                    </div>

                    <div class="card-body">
                        <!-- Mensajes de error/éxito -->
                        <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert alert-dismissible fade show mb-4">
                            <asp:Label ID="lblMensaje" runat="server" />
                            <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                        </asp:Panel>

                        <!-- Tabs de navegación -->
                        <ul class="nav nav-tabs mb-4" id="myTab" role="tablist">
                            <li class="nav-item" role="presentation">
                                <button class="nav-link active" id="perfil-tab" data-bs-toggle="tab" data-bs-target="#perfil" type="button" role="tab">
                                    <i class="fas fa-user me-2"></i>Datos Personales
                                </button>
                            </li>
                            <li class="nav-item" role="presentation">
                                <button class="nav-link" id="direcciones-tab" data-bs-toggle="tab" data-bs-target="#direcciones" type="button" role="tab">
                                    <i class="fas fa-map-marker-alt me-2"></i>Direcciones
                                </button>
                            </li>
                            <li class="nav-item" role="presentation">
                                <button class="nav-link" id="seguridad-tab" data-bs-toggle="tab" data-bs-target="#seguridad" type="button" role="tab">
                                    <i class="fas fa-lock me-2"></i>Seguridad
                                </button>
                            </li>
                        </ul>

                        <!-- Contenido de los tabs -->
                        <div class="tab-content" id="myTabContent">
                            <!-- Tab Datos Personales -->
                            <div class="tab-pane fade show active" id="perfil" role="tabpanel">
                                <div class="row">
                                    <div class="col-md-6 mb-3">
                                        <label class="form-label">Nombre *</label>
                                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                                            ControlToValidate="txtNombre"
                                            ErrorMessage="El nombre es requerido"
                                            CssClass="text-danger"
                                            Display="Dynamic"
                                            ValidationGroup="DatosPersonales" />
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label class="form-label">Apellido *</label>
                                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvApellido" runat="server"
                                            ControlToValidate="txtApellido"
                                            ErrorMessage="El apellido es requerido"
                                            CssClass="text-danger"
                                            Display="Dynamic"
                                            ValidationGroup="DatosPersonales" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6 mb-3">
                                        <label class="form-label">Email *</label>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                                            ControlToValidate="txtEmail"
                                            ErrorMessage="El email es requerido"
                                            CssClass="text-danger"
                                            Display="Dynamic"
                                            ValidationGroup="DatosPersonales" />
                                        <asp:RegularExpressionValidator ID="revEmail" runat="server"
                                            ControlToValidate="txtEmail"
                                            ErrorMessage="Formato de email inválido"
                                            CssClass="text-danger"
                                            Display="Dynamic"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            ValidationGroup="DatosPersonales" />
                                    </div>
                                    <div class="col-md-6 mb-3">
                                        <label class="form-label">Teléfono *</label>
                                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                            ControlToValidate="txtTelefono"
                                            ErrorMessage="El teléfono es requerido"
                                            CssClass="text-danger"
                                            Display="Dynamic"
                                            ValidationGroup="DatosPersonales" />
                                    </div>
                                </div>
                                <div class="text-end mt-3">
                                    <asp:Button ID="btnGuardarDatos" runat="server" Text="Guardar Cambios"
                                        CssClass="btn btn-primary"
                                        ValidationGroup="DatosPersonales"
                                        OnClick="btnGuardarDatos_Click" />
                                </div>
                            </div>

                            <!-- Tab Direcciones -->
                            <div class="tab-pane fade" id="direcciones" role="tabpanel">
                                <asp:Repeater ID="rptDirecciones" runat="server" OnItemCommand="rptDirecciones_ItemCommand">
                                    <ItemTemplate>
                                        <div class="card mb-3">
                                            <div class="card-body">
                                                <div class="d-flex justify-content-between align-items-center mb-2">
                                                    <h5 class="card-title mb-0">
                                                        <i class="fas fa-map-marker-alt me-2"></i>
                                                        <%# Eval("Calle") %> <%# Eval("Altura") %>
                                                    </h5>
                                                    <div>
                                                        <asp:LinkButton ID="lnkEditar" runat="server"
                                                            CssClass="btn btn-sm btn-outline-primary me-2"
                                                            CommandName="Editar"
                                                            CommandArgument='<%# Eval("Id") %>'>
                                                            <i class="fas fa-edit"></i> Editar
                                                        </asp:LinkButton>
                                                        <asp:LinkButton ID="lnkEliminar" runat="server"
                                                            CssClass="btn btn-sm btn-outline-danger"
                                                            CommandName="Eliminar"
                                                            CommandArgument='<%# Eval("Id") %>'
                                                            OnClientClick="return confirm('¿Está seguro que desea eliminar esta dirección?');">
                                                            <i class="fas fa-trash"></i>
                                                        </asp:LinkButton>
                                                    </div>
                                                </div>
                                                <p class="card-text">
                                                    <%# Eval("EntreCalles") %><br />
                                                    <%# (Eval("Piso") != DBNull.Value ? "Piso " + Eval("Piso") : "") %>
                                                    <%# (Eval("Departamento") != DBNull.Value ? (Eval("Piso") != DBNull.Value ? ", " : "") + "Depto " + Eval("Departamento") : "") %><br />
                                                    <%# Eval("Localidad") %>, <%# Eval("Provincia") %> (<%# Eval("CodigoPostal") %>)
                                                </p>
                                                <small class="text-muted"><%# Eval("Observaciones") %></small>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>

                                <asp:Panel ID="pnlNuevaDireccion" runat="server" Visible="false">
                                        <div class="card-body">
                                            <h5 class="card-title mb-4">
                                                <asp:Literal ID="ltlTituloDireccion" runat="server" Text="Nueva Dirección" />
                                            </h5>

                                            <div class="row">
                                                <div class="col-md-6 mb-3">
                                                    <label class="form-label">Calle *</label>
                                                    <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" />
                                                    <asp:RequiredFieldValidator ID="rfvCalle" runat="server"
                                                        ControlToValidate="txtCalle"
                                                        ErrorMessage="La calle es requerida"
                                                        CssClass="text-danger"
                                                        Display="Dynamic"
                                                        ValidationGroup="Direccion" />
                                                </div>
                                                <div class="col-md-3 mb-3">
                                                    <label class="form-label">Altura *</label>
                                                    <asp:TextBox ID="txtAltura" runat="server" CssClass="form-control" TextMode="Number" />
                                                    <asp:RequiredFieldValidator ID="rfvAltura" runat="server"
                                                        ControlToValidate="txtAltura"
                                                        ErrorMessage="La altura es requerida"
                                                        CssClass="text-danger"
                                                        Display="Dynamic"
                                                        ValidationGroup="Direccion" />
                                                    <asp:RangeValidator ID="rvAltura" runat="server"
                                                        ControlToValidate="txtAltura"
                                                        Type="Integer"
                                                        MinimumValue="1"
                                                        MaximumValue="99999"
                                                        ErrorMessage="Altura inválida"
                                                        CssClass="text-danger"
                                                        Display="Dynamic"
                                                        ValidationGroup="Direccion" />
                                                </div>
                                                <div class="col-md-3 mb-3">
                                                    <label class="form-label">Piso</label>
                                                    <asp:TextBox ID="txtPiso" runat="server" CssClass="form-control" TextMode="Number" />
                                                    <asp:RangeValidator ID="rvPiso" runat="server"
                                                        ControlToValidate="txtPiso"
                                                        Type="Integer"
                                                        MinimumValue="0"
                                                        MaximumValue="99"
                                                        ErrorMessage="Piso inválido"
                                                        CssClass="text-danger"
                                                        Display="Dynamic"
                                                        ValidationGroup="Direccion" />
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-6 mb-3">
                                                    <label class="form-label">Entre Calles</label>
                                                    <asp:TextBox ID="txtEntreCalles" runat="server" CssClass="form-control" />
                                                </div>
                                                <div class="col-md-6 mb-3">
                                                    <label class="form-label">Departamento</label>
                                                    <asp:TextBox ID="txtDepartamento" runat="server" CssClass="form-control" MaxLength="20" />
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-6 mb-3">
                                                    <label class="form-label">Provincia *</label>
                                                    <asp:DropDownList ID="ddlProvincia" runat="server" CssClass="form-select">
                                                        <asp:ListItem Text="Seleccione una provincia" Value="" />
                                                        <asp:ListItem Text="Buenos Aires" Value="Buenos Aires" />
                                                        <asp:ListItem Text="CABA" Value="CABA" />
                                                        <asp:ListItem Text="Catamarca" Value="Catamarca" />
                                                        <asp:ListItem Text="Chaco" Value="Chaco" />
                                                        <asp:ListItem Text="Chubut" Value="Chubut" />
                                                        <asp:ListItem Text="Córdoba" Value="Córdoba" />
                                                        <asp:ListItem Text="Corrientes" Value="Corrientes" />
                                                        <asp:ListItem Text="Entre Ríos" Value="Entre Ríos" />
                                                        <asp:ListItem Text="Formosa" Value="Formosa" />
                                                        <asp:ListItem Text="Jujuy" Value="Jujuy" />
                                                        <asp:ListItem Text="La Pampa" Value="La Pampa" />
                                                        <asp:ListItem Text="La Rioja" Value="La Rioja" />
                                                        <asp:ListItem Text="Mendoza" Value="Mendoza" />
                                                        <asp:ListItem Text="Misiones" Value="Misiones" />
                                                        <asp:ListItem Text="Neuquén" Value="Neuquén" />
                                                        <asp:ListItem Text="Río Negro" Value="Río Negro" />
                                                        <asp:ListItem Text="Salta" Value="Salta" />
                                                        <asp:ListItem Text="San Juan" Value="San Juan" />
                                                        <asp:ListItem Text="San Luis" Value="San Luis" />
                                                        <asp:ListItem Text="Santa Cruz" Value="Santa Cruz" />
                                                        <asp:ListItem Text="Santa Fe" Value="Santa Fe" />
                                                        <asp:ListItem Text="Santiago del Estero" Value="Santiago del Estero" />
                                                        <asp:ListItem Text="Tierra del Fuego" Value="Tierra del Fuego" />
                                                        <asp:ListItem Text="Tucumán" Value="Tucumán" />
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvProvincia" runat="server"
                                                        ControlToValidate="ddlProvincia"
                                                        ErrorMessage="La provincia es requerida"
                                                        CssClass="text-danger"
                                                        Display="Dynamic"
                                                        ValidationGroup="Direccion" />
                                                </div>
                                                <div class="col-md-6 mb-3">
                                                    <label class="form-label">Localidad *</label>
                                                    <asp:TextBox ID="txtLocalidad" runat="server" CssClass="form-control" />
                                                    <asp:RequiredFieldValidator ID="rfvLocalidad" runat="server"
                                                        ControlToValidate="txtLocalidad"
                                                        ErrorMessage="La localidad es requerida"
                                                        CssClass="text-danger"
                                                        Display="Dynamic"
                                                        ValidationGroup="Direccion" />
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-4 mb-3">
                                                    <label class="form-label">Código Postal *</label>
                                                    <asp:TextBox ID="txtCP" runat="server" CssClass="form-control" MaxLength="8" />
                                                    <asp:RequiredFieldValidator ID="rfvCP" runat="server"
                                                        ControlToValidate="txtCP"
                                                        ErrorMessage="El código postal es requerido"
                                                        CssClass="text-danger"
                                                        Display="Dynamic"
                                                        ValidationGroup="Direccion" />
                                                    <asp:RegularExpressionValidator ID="revCP" runat="server"
                                                        ControlToValidate="txtCP"
                                                        ValidationExpression="^[0-9]{4}$"
                                                        ErrorMessage="Código postal inválido"
                                                        CssClass="text-danger"
                                                        Display="Dynamic"
                                                        ValidationGroup="Direccion" />
                                                </div>
                                                <div class="col-md-8 mb-3">
                                                    <label class="form-label">Observaciones</label>
                                                    <asp:TextBox ID="txtObservaciones" runat="server" CssClass="form-control"
                                                        TextMode="MultiLine" Rows="2" />
                                                </div>
                                            </div>

                                            <div class="d-flex justify-content-end gap-2 mt-3">
                                                <asp:Button ID="btnCancelarDireccion" runat="server" Text="Cancelar"
                                                    CssClass="btn btn-secondary"
                                                    CausesValidation="false"
                                                    OnClick="btnCancelarDireccion_Click" />
                                                <asp:Button ID="btnGuardarDireccion" runat="server" Text="Guardar Dirección"
                                                    CssClass="btn btn-primary"
                                                    ValidationGroup="Direccion"
                                                    OnClick="btnGuardarDireccion_Click" />
                                            </div>
                                        </div>
                                    </asp:Panel>
<%--                                </asp:Panel>--%>

                                <div class="text-end mt-3">
                                    <asp:Button ID="btnNuevaDireccion" runat="server" Text="Agregar Nueva Dirección"
                                        CssClass="btn btn-success"
                                        OnClick="btnNuevaDireccion_Click" />
                                </div>
                            </div>

                            <!-- Tab Seguridad -->
                            <div class="tab-pane fade" id="seguridad" role="tabpanel">
                                <div class="row justify-content-center">
                                    <div class="col-md-8">
                                        <div class="mb-3">
                                            <label class="form-label">Contraseña Actual *</label>
                                            <asp:TextBox ID="txtPasswordActual" runat="server"
                                                CssClass="form-control" TextMode="Password" />
                                            <asp:RequiredFieldValidator ID="rfvPasswordActual" runat="server"
                                                ControlToValidate="txtPasswordActual"
                                                ErrorMessage="La contraseña actual es requerida"
                                                CssClass="text-danger"
                                                Display="Dynamic"
                                                ValidationGroup="Seguridad" />
                                        </div>
                                        <div class="mb-3">
                                            <label class="form-label">Nueva Contraseña *</label>
                                            <asp:TextBox ID="txtNuevaPassword" runat="server"
                                                CssClass="form-control" TextMode="Password" />
                                            <asp:RequiredFieldValidator ID="rfvNuevaPassword" runat="server"
                                                ControlToValidate="txtNuevaPassword"
                                                ErrorMessage="La nueva contraseña es requerida"
                                                CssClass="text-danger"
                                                Display="Dynamic"
                                                ValidationGroup="Seguridad" />
                                        </div>
                                        <div class="mb-3">
                                            <label class="form-label">Confirmar Nueva Contraseña *</label>
                                            <asp:TextBox ID="txtConfirmarPassword" runat="server"
                                                CssClass="form-control" TextMode="Password" />
                                            <asp:RequiredFieldValidator ID="rfvConfirmarPassword" runat="server"
                                                ControlToValidate="txtConfirmarPassword"
                                                ErrorMessage="Debe confirmar la nueva contraseña"
                                                CssClass="text-danger"
                                                Display="Dynamic"
                                                ValidationGroup="Seguridad" />
                                            <asp:CompareValidator ID="cvPassword" runat="server"
                                                ControlToValidate="txtConfirmarPassword"
                                                ControlToCompare="txtNuevaPassword"
                                                ErrorMessage="Las contraseñas no coinciden"
                                                CssClass="text-danger"
                                                Display="Dynamic"
                                                ValidationGroup="Seguridad" />
                                        </div>
                                        <div class="text-end mt-3">
                                            <asp:Button ID="btnCambiarPassword" runat="server"
                                                Text="Cambiar Contraseña"
                                                CssClass="btn btn-primary"
                                                ValidationGroup="Seguridad"
                                                OnClick="btnCambiarPassword_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
