<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Formulario web1.aspx.cs" Inherits="tp_TCP_equipo_19B.Formulario_web1" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
    <!-- Puedes colocar contenido aquí para el encabezado, si es necesario -->
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <style>
        /* Estilos generales */
        .contenedor {
            background-color: #f8f9fa;
            padding: 20px;
            border: 1px solid #dee2e6;
            border-radius: 5px;
            margin-bottom: 20px;
        }

        .form-contenedor {
            max-width: 600px;
            margin: auto;
        }

        .form-contenedor2 {
            max-width: 600px;
            margin: auto;
            background-color: #fff;
            padding: 20px;
            border: 1px solid #ced4da;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            margin-top: 20px;
        }

        /* Estilos para etiquetas y campos de formulario */
        .form-label {
            font-weight: bold;
            margin-bottom: 5px;
        }

        .form-control {
            width: 100%;
            padding: 8px;
            font-size: 14px;
            border: 1px solid #ced4da;
            border-radius: 4px;
            box-sizing: border-box;
        }

        /* Estilos para botones */
        .btn {
            background-color: #007bff;
            color: #fff;
            border: none;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
            text-align: center;
            display: inline-block;
        }

        .btn:hover {
            background-color: #0056b3;
        }
    </style>

    <section class="contenedor">
        <div class="form-contenedor">
            <h1>Entrega</h1>

            <div class="mb-3">
                <label class="form-label" for="ddlPais">País</label>
                <asp:DropDownList runat="server" ID="ddlPais" CssClass="form-control">
                    <asp:ListItem Text="Argentina" Value="Argentina" />
                    <asp:ListItem Text="Chile" Value="Chile" />
                    <asp:ListItem Text="Perú" Value="Perú" />
                    <asp:ListItem Text="Uruguay" Value="Uruguay" />
                    <asp:ListItem Text="Paraguay" Value="Paraguay" />
                    <asp:ListItem Text="México" Value="México" />
                    <asp:ListItem Text="Venezuela" Value="Venezuela" />
                    <asp:ListItem Text="Colombia" Value="Colombia" />
                    <asp:ListItem Text="Bolivia" Value="Bolivia" />
                </asp:DropDownList>
            </div>

            <div class="mb-3">
                <label class="form-label" for="txtCodigoPostal">Código Postal</label>
                <asp:TextBox runat="server" ID="txtCodigoPostal" CssClass="form-control" />
            </div>
        </div>
    </section>

    <section class="contenedor">
        <div class="form-contenedor">
            <h1>Datos de Facturación</h1>

            <div class="mb-3">
                <label class="form-label" for="DropDownList1">País</label>
                <asp:DropDownList runat="server" ID="DropDownList1" CssClass="form-control">
                    <asp:ListItem Text="Argentina" Value="Argentina" />
                    <asp:ListItem Text="Chile" Value="Chile" />
                    <asp:ListItem Text="Perú" Value="Perú" />
                    <asp:ListItem Text="Uruguay" Value="Uruguay" />
                    <asp:ListItem Text="Paraguay" Value="Paraguay" />
                    <asp:ListItem Text="México" Value="México" />
                    <asp:ListItem Text="Venezuela" Value="Venezuela" />
                    <asp:ListItem Text="Colombia" Value="Colombia" />
                    <asp:ListItem Text="Bolivia" Value="Bolivia" />
                </asp:DropDownList>
            </div>

            <h2>Persona que pagará el envío</h2>

            <div class="mb-3">
                <label class="form-label" for="txtNombre">Nombre</label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label class="form-label" for="txtApellido">Apellido</label>
                <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label class="form-label" for="txtTelefono">Teléfono</label>
                <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" TextMode="SingleLine" />
            </div>

            <div class="mb-3">
                <label class="form-label" for="txtMail">Correo Electrónico</label>
                <asp:TextBox runat="server" ID="txtMail" CssClass="form-control" TextMode="Email" />
            </div>
        </div>
    </section>

    <section class="contenedor">
        <div class="form-contenedor">
            <h1>Método de Pagos</h1>

            <div class="mb-3">
                <asp:CheckBox Text="Transferencia" runat="server" />
                <asp:CheckBox Text="Mercado Pago" runat="server" />
            </div>

            <asp:Button Text="Continuar" runat="server" ID="btnContinuar" OnClick="btnEntregaContinuar_Click" CssClass="btn btn-primary" />
        </div>
    </section>
</asp:Content>
