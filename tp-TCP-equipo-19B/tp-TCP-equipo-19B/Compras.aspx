<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Compras.aspx.cs" Inherits="tp_TCP_equipo_19B.Formulario_web1" %>

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

        .cart-summary {
            margin: 20px 0;
            padding: 20px;
            background-color: #f8f9fa;
            border-radius: 8px;
        }

        .cart-item {
            padding: 10px;
            margin-bottom: 10px;
        }

        .totals {
            margin-top: 20px;
            padding-top: 20px;
            border-top: 2px solid #dee2e6;
        }

        .button-group {
            display: flex;
            gap: 10px;
            justify-content: space-between;
            margin-top: 20px;
        }

        .addresses-container {
            margin: 20px 0;
        }

        .address-card {
            border: 1px solid #dee2e6;
            border-radius: 8px;
            padding: 15px;
            margin-bottom: 15px;
            background-color: #fff;
            transition: all 0.3s ease;
        }

            .address-card:hover {
                box-shadow: 0 2px 5px rgba(0,0,0,0.1);
            }

        .address-content {
            display: flex;
            align-items: flex-start;
            gap: 15px;
        }

        .address-radio {
            margin-top: 5px;
        }

        .address-details {
            flex-grow: 1;
        }

            .address-details h5 {
                margin-bottom: 8px;
                color: #333;
            }

            .address-details p {
                margin-bottom: 5px;
                color: #666;
            }
    </style>

    <section class="contenedor">
        <div class="form-contenedor">
            <!-- Resumen del Carrito -->
            <h2 class="Center">Resumen de la Compra</h2>
            <div class="cart-summary">
                <asp:Repeater ID="rptCarrito" runat="server">
                    <ItemTemplate>
                        <div class="cart-item">
                            <div class="product-info">
                                <h5><%# Eval("Nombre") %></h5>
                                <p>Cantidad: <%# Eval("Cantidad") %></p>
                                <p>Precio unitario: $<%# Eval("Precio") %></p>
                                <p>Subtotal: $<%# Convert.ToDecimal(Eval("Precio")) * Convert.ToInt32(Eval("Cantidad")) %></p>
                            </div>
                        </div>
                        <div class="separador"></div>
                    </ItemTemplate>
                </asp:Repeater>
                <div class="totals">
                    <p>Subtotal: $<asp:Label ID="lblSubtotal" runat="server" /></p>
                    <p>Costo de envío: $<asp:Label ID="lblCostoEnvio" runat="server" /></p>
                    <h4>Total: $<asp:Label ID="lblTotal" runat="server" /></h4>
                </div>
            </div>

            <!-- Dirección de Envío -->
            <h2 class="Center">Dirección de Envío</h2>
            <div class="addresses-container">
                <asp:Repeater ID="rptDirecciones" runat="server">
                    <ItemTemplate>
                        <div class="address-card">
                            <div class="address-content">
                                <asp:RadioButton ID="rbDireccion" runat="server"
                                    GroupName="direccion"
                                    CssClass="address-radio"
                                    Value='<%# Eval("Id") %>' />
                                <div class="address-details">
                                    <h5><%# Eval("Calle") %> <%# Eval("Altura") %></h5>
                                    <p>
                                        <%# Eval("EntreCalles") %><br />
                                        <%# (Eval("Piso") != DBNull.Value ? "Piso " + Eval("Piso") : "") %>
                                        <%# (Eval("Departamento") != DBNull.Value ? (Eval("Piso") != DBNull.Value ? ", " : "") + "Depto " + Eval("Departamento") : "") %><br />
                                        <%# Eval("Localidad") %>, <%# Eval("Provincia") %> (<%# Eval("CodigoPostal") %>)
                       
                                    </p>
                                    <small class="text-muted"><%# Eval("Observaciones") %></small>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <!-- Método de Envío -->
            <h2 class="Center">Método de Envío</h2>
            <div class="shipping-methods">
                <asp:Repeater ID="rptMetodosEnvio" runat="server">
                    <ItemTemplate>
                        <div class="payment-card">
                            <label>
                                <asp:RadioButton ID="rbEnvio" runat="server"
                                    GroupName="metodoEnvio"
                                    AutoPostBack="true"
                                    OnCheckedChanged="rbEnvio_CheckedChanged"
                                    Value='<%# Eval("Id") %>' />
                                <span><%# Eval("Nombre") %> - $<%# Eval("Costo") %></span>
                            </label>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <!-- Método de Pago -->
            <h2 class="Center">Método de Pago</h2>
            <div class="payment-methods">
                <asp:Repeater ID="rptFormasPago" runat="server">
                    <ItemTemplate>
                        <div class="payment-card">
                            <label>
                                <asp:RadioButton ID="rbPago" runat="server"
                                    GroupName="formaPago"
                                    Value='<%# Eval("Id") %>' />
                                <span><%# Eval("Nombre") %></span>
                            </label>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <!-- Botones -->
            <div class="button-group">
                <asp:Button ID="btnVolver" runat="server" Text="Volver al Carrito"
                    CssClass="btn btn-secondary" OnClick="btnVolver_Click"
                    CausesValidation="false" />
                <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar Compra"
                    CssClass="btn btn-success" OnClick="btnConfirmar_Click" />
            </div>

            <!-- Panel de Mensajes -->
            <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert mt-3">
                <asp:Label ID="lblMensaje" runat="server" />
            </asp:Panel>
        </div>
    </section>

</asp:Content>
