<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="tp_TCP_equipo_19B.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
        <div>
            <h2>Iniciar Sesión</h2>

            <asp:Panel ID="pnlMensajes" runat="server" Visible="false">
                <asp:Label ID="lblMensaje" runat="server" />
            </asp:Panel>

            <div>
                <asp:Label ID="lblMail" runat="server" Text="Email:" />
                <asp:TextBox ID="txtMail" runat="server" />
                <asp:RequiredFieldValidator ID="rfvMail" runat="server"
                    ControlToValidate="txtMail"
                    ErrorMessage="El email es requerido"
                    Display="Dynamic" />
                <asp:RegularExpressionValidator ID="revMail" runat="server"
                    ControlToValidate="txtMail"
                    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ErrorMessage="Email inválido"
                    Display="Dynamic" />
            </div>

            <div>
                <asp:Label ID="lblPassword" runat="server" Text="Contraseña:" />
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                    ControlToValidate="txtPassword"
                    ErrorMessage="La contraseña es requerida"
                    Display="Dynamic" />
            </div>

            <div>
                <asp:Button ID="btnLogin" runat="server" Text="Iniciar Sesión" OnClick="btnLogin_Click" />
            </div>
            <div>
                <h3>Registrarse</h3>
                <asp:Label ID="lblNombre" runat="server" Text="Ingresa tu nombre:" />
                <asp:TextBox ID="tbxNombre" runat="server" />
                <asp:Label ID="lblApellido" runat="server" Text="Ingresa tu apellido:" />
                <asp:TextBox ID="tbxApellido" runat="server" />
                <asp:Label ID="lblTelefono" runat="server" Text="Ingresa tu celular:" />
                <asp:TextBox ID="tbxTelefono" runat="server" />
            </div>
            <div>
                <asp:Button ID="btnRegister" runat="server" Text="Registrarse" OnClick="btnRegister_Click" />
            </div>
        </div>
</asp:Content>
