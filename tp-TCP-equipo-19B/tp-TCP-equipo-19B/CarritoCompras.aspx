<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarritoCompras.aspx.cs" Inherits="tp_TCP_equipo_19B.Formulario_web14" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <style>
        .titulo-catalogo {
            text-align: center;
            font-size: 2rem;
            color: #000000;
            margin-top: 15px;
            margin-bottom: 15px;
            text-shadow: 2px 2px 2px rgba(0, 0, 0, 0.2);
        }
        .carrito-footer {
            display: flex;
            justify-content: space-between;
            margin-top: 20px;
            padding: 10px;
            border-top: 1px solid #ddd;
        }
        .carrito-footer .total {
            font-size: 1.5rem;
            font-weight: bold;
        }
        .error-message {
            color: red;
            font-size: 1rem;
            margin-top: 10px;
        }
    </style>

    <h1 class="titulo-catalogo">Carrito de compras</h1>

    <asp:UpdatePanel ID="updatePanelCarrito" runat="server">
        <ContentTemplate>
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th class="d-none">Id</th>
                        <th>Nombre</th>
                        <th>Descripción</th>
                        <th>Precio</th>               
                        <th>Cantidad</th>
                        <th>Eliminar</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repeaterCarrito" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="d-none" name="id"><%# Eval("Id_producto") %></td>                       
                                <td><%# Eval("Nombre") %></td>
                                <td><%# Eval("Descripcion") %></td>
                                <td><%# Eval("Precio") %></td>
                                <td>
                                    <asp:Button ID="btnDisminuirCantidad" CssClass="btn btn-info" runat="server" Text="-" OnClick="btnDisminuirCantidad_Click" CommandArgument='<%# Eval("Id_producto") %>' CommandName="disminuirCantidad" />
                                    <asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("Cantidad") %>'></asp:Label>
                                    <asp:Button ID="btnAumentarCantidad" CssClass="btn btn-info" runat="server" Text="+" OnClick="btnAumentarCantidad_Click" CommandArgument='<%# Eval("Id_producto") %>' CommandName="aumentarCantidad" />
                                </td>
                                <td>
                                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-danger" Text="Quitar" OnClick="btnEliminar_Click" CommandArgument='<%# Eval("Id_producto") %>' CommandName="idArticulo" />
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>

            <div class="carrito-footer">
                <div class="total">
                    Total: $  
                    <asp:Label ID="lblPrecioTotal" runat="server" Text="0"></asp:Label>
                </div>
                <asp:Button ID="btnComprar" OnClick="btnComprar_Click" runat="server" Text="Comprar" CssClass="btn btn-success" />
            </div>

            <asp:Label ID="lblError" runat="server" CssClass="error-message" Text="" Visible="false"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
