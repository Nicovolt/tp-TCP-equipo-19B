<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="tp_TCP_equipo_19B.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container mt-5">
        <div class="row justify-content-center">
            <div class="col-md-6">
                <asp:Panel ID="pnlMensajes" runat="server" Visible="false" CssClass="alert alert-danger">
                    <asp:Label ID="lblMensaje" runat="server" />
                </asp:Panel>

                <!-- Tabs de navegación -->
                <ul class="nav nav-tabs mb-4" id="loginTabs" role="tablist">
                    <li class="nav-item" role="presentation">
                        <button class="nav-link active" id="login-tab" data-bs-toggle="tab" 
                                data-bs-target="#login" type="button" role="tab">Iniciar Sesión</button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link" id="register-tab" data-bs-toggle="tab" 
                                data-bs-target="#register" type="button" role="tab">Registrarse</button>
                    </li>
                </ul>

                <!-- Contenido de los tabs -->
                <div class="tab-content" id="loginTabsContent">
                    <!-- Tab de Login -->
                    <div class="tab-pane fade show active" id="login" role="tabpanel">
                        <div class="card">
                            <div class="card-body">
                                <h3 class="card-title text-center mb-4">Iniciar Sesión</h3>
                                <div class="mb-3">
                                    <asp:Label ID="lblMail" runat="server" Text="Email:" CssClass="form-label" />
                                    <asp:TextBox ID="txtMail" runat="server" CssClass="form-control" placeholder="nombre@ejemplo.com" />
                                    <asp:RequiredFieldValidator ID="rfvMail" runat="server"
                                        ControlToValidate="txtMail"
                                        ErrorMessage="El email es requerido"
                                        Display="Dynamic"
                                        ValidationGroup="LoginGroup"
                                        CssClass="text-danger" />

                                    <asp:RegularExpressionValidator ID="revMail" runat="server"
                                        ControlToValidate="txtMail"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ErrorMessage="Email inválido"
                                        Display="Dynamic"
                                        ValidationGroup="LoginGroup"
                                        CssClass="text-danger" />
                                </div>
                                <div class="mb-3">
                                    <asp:Label ID="lblPassword" runat="server" Text="Contraseña:" CssClass="form-label" />
                                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                                        ControlToValidate="txtPassword"
                                        ErrorMessage="La contraseña es requerida"
                                        Display="Dynamic"
                                        ValidationGroup="LoginGroup"
                                        CssClass="text-danger" />
                                </div>
                                <div class="d-grid">
                                    <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión" OnClick="btnLogin_Click" ValidationGroup="LoginGroup" CssClass="btn btn-primary" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Tab de Registro -->
                    <div class="tab-pane fade" id="register" role="tabpanel">
                        <div class="card">
                            <div class="card-body">
                                <h3 class="card-title text-center mb-4">Registrarse</h3>
                                <div class="mb-3">
                                    <asp:Label ID="lblNombre" runat="server" Text="Nombre:" CssClass="form-label" />
                                    <asp:TextBox ID="tbxNombre" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                                        ControlToValidate="tbxNombre"
                                        ErrorMessage="El nombre es requerido"
                                        Display="Dynamic"
                                        ValidationGroup="RegisterGroup"
                                        CssClass="text-danger" />
                                </div>
                                <div class="mb-3">
                                    <asp:Label ID="lblApellido" runat="server" Text="Apellido:" CssClass="form-label" />
                                    <asp:TextBox ID="tbxApellido" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvApellido" runat="server"
                                        ControlToValidate="tbxApellido"
                                        ErrorMessage="El apellido es requerido"
                                        Display="Dynamic"
                                        ValidationGroup="RegisterGroup"
                                        CssClass="text-danger" />
                                </div>
                                <div class="mb-3">
                                    <asp:Label ID="lblTelefono" runat="server" Text="Celular:" CssClass="form-label" />
                                    <asp:TextBox ID="tbxTelefono" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvTelefono" runat="server"
                                        ControlToValidate="tbxTelefono"
                                        ErrorMessage="El teléfono es requerido"
                                        Display="Dynamic"
                                        ValidationGroup="RegisterGroup"
                                        CssClass="text-danger" />
                                </div>
                                <div class="mb-3">
                                    <asp:Label ID="lblRegEmail" runat="server" Text="Email:" CssClass="form-label" />
                                    <asp:TextBox ID="txtRegEmail" runat="server" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvRegEmail" runat="server"
                                        ControlToValidate="txtRegEmail"
                                        ErrorMessage="El email es requerido"
                                        Display="Dynamic"
                                        ValidationGroup="RegisterGroup"
                                        CssClass="text-danger" />
                                </div>
                                <div class="mb-3">
                                    <asp:Label ID="lblRegPassword" runat="server" Text="Contraseña:" CssClass="form-label" />
                                    <asp:TextBox ID="txtRegPassword" runat="server" TextMode="Password" CssClass="form-control" />
                                    <asp:RequiredFieldValidator ID="rfvRegPassword" runat="server"
                                        ControlToValidate="txtRegPassword"
                                        ErrorMessage="La contraseña es requerida"
                                        Display="Dynamic"
                                        ValidationGroup="RegisterGroup"
                                        CssClass="text-danger" />
                                </div>
                                <div class="d-grid">
                                    <asp:Button ID="btnRegister" runat="server" Text="Registrarse" ValidationGroup="RegisterGroup" OnClick="btnRegister_Click" CssClass="btn btn-success" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        // Función para ocultar el mensaje
        function ocultarMensaje() {
            var panel = document.getElementById('<%= pnlMensajes.ClientID %>');
            if (panel) {
                panel.style.display = 'none';
            }
        }

        // Ocultar mensaje después de 5 segundos
        setTimeout(function() {
            ocultarMensaje();
        }, 5000);

        // Ocultar mensaje al cambiar de tab
        document.querySelectorAll('[data-bs-toggle="tab"]').forEach(function(element) {
            element.addEventListener('shown.bs.tab', function (e) {
                ocultarMensaje();
            });
        });

    </script>
</asp:Content>
