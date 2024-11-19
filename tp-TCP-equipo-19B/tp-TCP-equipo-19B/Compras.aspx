<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Formulario web1.aspx.cs" Inherits="tp_TCP_equipo_19B.Formulario_web1" %>

<asp:Content ID="ContentHead" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="ContentBody" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <style>
        .contenedor {
            background-color: #f7f7f7;
            padding: 40px 60px;
            border-radius: 8px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            margin-bottom: 30px;
        }

        .form-contenedor {
            max-width: 800px;
            margin: auto;
            padding: 20px;
            background-color: #ffffff;
            border: 1px solid #e3e4e8;
            border-radius: 8px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
        }

        h2 {
            color: #333;
            margin-bottom: 20px;
            margin-top: 10px
        }

        .separador {
            height: 1px;
            background-color: #ddd; 
            margin: 20px 0; 
            box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1); 
        }

        .form-label {
            font-weight: 600;
            margin-bottom: 8px;
            display: inline-block;
            color: #555;
        }

        .form-control {
            width: 100%;
            padding: 12px;
            margin: 8px 0;
            font-size: 16px;
            border: 1px solid #ccc;
            border-radius: 4px;
            background-color: #f9f9f9;
            box-sizing: border-box;
        }

        .form-control:focus {
            border-color: #007bff;
            outline: none;
        }

        .form-row {
            display: flex;
            gap: 20px;
            justify-content: space-between;
        }

        .form-row .form-group {
            flex: 1;
        }

        .btn {
            background-color: #007bff;
            color: #fff;
            border: none;
            padding: 12px 25px;
            cursor: pointer;
            border-radius: 5px;
            font-size: 16px;
            margin-top: 20px;
            width: 100%;
        }

        .btn:hover {
            background-color: #0056b3;
        }

        .checkbox-group {
            display: flex;
            gap: 20px;
            margin-bottom: 20px;
        }

        .Center {
            text-align: center;
        }

        .payment-methods {
            display: flex;
            gap: 20px;
            justify-content: center;
            margin: 20px 0;
        }

        .payment-card {
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 15px 20px;
            background: linear-gradient(135deg, #007bff, #00d4ff);
            color: #fff;
            border-radius: 10px;
            box-shadow: 0 4px 10px rgba(0, 0, 0, 0.2);
            cursor: pointer;
            transition: transform 0.3s ease, box-shadow 0.3s ease;
        }

        .payment-card:hover {
            transform: scale(1.05);
            box-shadow: 0 8px 20px rgba(0, 0, 0, 0.3);
        }

        .payment-card input[type="checkbox"] {
            margin-right: 10px;
            accent-color: #fff;
            transform: scale(1.5); 
        }

        .text-danger {
            color: #d9534f;
            font-size: 14px;
            margin-top: 5px;
        }
    </style>

    <section class="contenedor">
    <div class="form-contenedor">
        <h2 class="Center">Datos de Facturación</h2>

        <!-- Datos de Entrega -->
        <div class="form-row">
            <div class="form-group">
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
                <asp:Label runat="server" ID="lblErrorPais" CssClass="text-danger" Visible="False" Text="Debe seleccionar un país."></asp:Label>
            </div>
            <div class="form-group">
                <label class="form-label" for="txtCodigoPostal">Código Postal</label>
                <asp:TextBox runat="server" ID="txtCodigoPostal" CssClass="form-control" />
                <asp:Label runat="server" ID="lblErrorCodigoPostal" CssClass="text-danger" Visible="False" Text="El código postal no puede estar vacío."></asp:Label>
            </div>
        </div>

        <!-- Datos de Facturación -->
        <div class="form-row">
            <div class="form-group">
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
                <asp:Label runat="server" ID="lblErrorPaisFacturacion" CssClass="text-danger" Visible="False" Text="Debe seleccionar un país de facturación."></asp:Label>
            </div>
        </div>

        <div class="form-row">
            <div class="form-group">
                <label class="form-label" for="txtNombre">Nombre</label>
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
                <asp:Label runat="server" ID="lblErrorNombre" CssClass="text-danger" Visible="False" Text="El nombre no puede estar vacío."></asp:Label>
            </div>
            <div class="form-group">
                <label class="form-label" for="txtApellido">Apellido</label>
                <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" />
                <asp:Label runat="server" ID="lblErrorApellido" CssClass="text-danger" Visible="False" Text="El apellido no puede estar vacío."></asp:Label>
            </div>
        </div>

        <div class="form-row">
            <div class="form-group">
                <label class="form-label" for="txtTelefono">Teléfono</label>
                <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" TextMode="SingleLine" />
                <asp:Label runat="server" ID="lblErrorTelefono" CssClass="text-danger" Visible="False" Text="El teléfono no puede estar vacío."></asp:Label>
            </div>
            <div class="form-group">
                <label class="form-label" for="txtMail">Correo Electrónico</label>
                <asp:TextBox runat="server" ID="txtMail" CssClass="form-control" TextMode="Email" />
                <asp:Label runat="server" ID="lblErrorMail" CssClass="text-danger" Visible="False" Text="El correo electrónico no es válido."></asp:Label>
            </div>
        </div>

        <!--  Pago -->
        <h2 class="Center">Método de Pago</h2>
        <div class="payment-methods">
            <!-- opc 1 -->
            <div class="payment-card">
                <label>
                    <asp:RadioButton runat="server" GroupName="metodoPago" ID="rbTransferencia" />
                    <span>Transferencia</span>
                </label>
            </div>

            <!-- opc 2 -->
            <div class="payment-card">
                <label>
                    <asp:RadioButton runat="server" GroupName="metodoPago" ID="rbMercadoPago" />
                    <span>Mercado Pago</span>
                </label>
            </div>
        </div>

        <asp:Button Text="Continuar" runat="server" ID="btnContinuar" OnClick="btnEntregaContinuar_Click" CssClass="btn" />
    </div>
</section>

</asp:Content>
