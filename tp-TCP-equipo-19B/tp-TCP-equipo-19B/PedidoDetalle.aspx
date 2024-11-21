<%@ Page Title="Detalle de Pedido" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PedidoDetalle.aspx.cs" Inherits="tp_TCP_equipo_19B.PedidoDetalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container py-5">
        <div class="row">
            <div class="col-lg-8">
                <div class="card mb-4 shadow-sm">
                    <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
                        <h4 class="mb-0">Pedido #<asp:Literal ID="ltlNroPedido" runat="server" /></h4>
                        <span class="badge" runat="server" id="badgeEstado"></span>
                    </div>
                    <div class="card-body">
                        <div class="mb-4">
                            <p class="mb-1"><strong>Fecha:</strong>
                                <asp:Literal ID="ltlFecha" runat="server" /></p>
                            <p class="mb-1"><strong>Estado:</strong>
                                <asp:Literal ID="ltlEstado" runat="server" /></p>
                            <p class="mb-0"><strong>Última actualización:</strong>
                                <asp:Literal ID="ltlUltimaActualizacion" runat="server" /></p>
                        </div>

                        <h5 class="mb-3">Productos</h5>
                        <div class="table-responsive">
                            <asp:Repeater ID="rptDetalles" runat="server">
                                <HeaderTemplate>
                                    <table class="table">
                                        <thead>
                                            <tr>
                                                <th>Producto</th>
                                                <th>Cantidad</th>
                                                <th class="text-end">Precio Unit.</th>
                                                <th class="text-end">Subtotal</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("NombreProducto") %></td>
                                        <td><%# Eval("Cantidad") %></td>
                                        <td class="text-end">$<%# Convert.ToDecimal(Eval("PrecioUnitario")).ToString("N2") %></td>
                                        <td class="text-end">$<%# Convert.ToDecimal(Eval("Subtotal")).ToString("N2") %></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                        </tbody>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-lg-4">
                <div class="card mb-4 shadow-sm">
                    <div class="card-header">
                        <h5 class="mb-0">Información de Envío</h5>
                    </div>
                    <div class="card-body">
                        <h6 class="mb-2">Método de envío</h6>
                        <p class="mb-3">
                            <asp:Literal ID="ltlMetodoEnvio" runat="server" />
                            - 
                            $<asp:Literal ID="ltlCostoEnvio" runat="server" />
                        </p>

                        <h6 class="mb-2">Dirección de entrega</h6>
                        <p class="mb-0">
                            <asp:Literal ID="ltlDireccion" runat="server" />
                        </p>
                    </div>
                </div>

                <div class="card mb-4 shadow-sm">
                    <div class="card-header">
                        <h5 class="mb-0">Información de Pago</h5>
                    </div>
                    <div class="card-body">
                        <p class="mb-2"><strong>Método:</strong>
                            <asp:Literal ID="ltlMetodoPago" runat="server" /></p>
                        <hr>
                        <div class="d-flex justify-content-between mb-2">
                            <span>Subtotal:</span>
                            <strong>$<asp:Literal ID="ltlSubtotal" runat="server" /></strong>
                        </div>
                        <div class="d-flex justify-content-between mb-2">
                            <span>Envío:</span>
                            <strong>$<asp:Literal ID="ltlEnvio" runat="server" /></strong>
                        </div>
                        <div class="d-flex justify-content-between">
                            <span class="h5 mb-0">Total:</span>
                            <strong class="h5 mb-0">$<asp:Literal ID="ltlTotal" runat="server" /></strong>
                        </div>
                    </div>
                </div>

                <asp:Button ID="btnVolver" runat="server" Text="Volver a Mis Pedidos"
                    CssClass="btn btn-secondary w-100" OnClick="btnVolver_Click" />
            </div>
        </div>
    </div>

    <style>
        .badge {
            padding: 8px 12px;
            font-size: 0.875rem;
        }
        .badge-pending { background-color: #0dcaf0; color: #000; }
        .badge-success { background-color: #198754; color: #fff; }
        .badge-danger { background-color: #dc3545; color: #fff; }
        .badge-dark { background-color: #212529; color: #fff; }
        .badge-info { background-color: #0dcaf0; color: #000; }
        .badge-primary { background-color: #0d6efd; color: #fff; }
        .badge-purple { background-color: #6f42c1; color: #fff; }
        .badge-secondary { background-color: #6c757d; color: #fff; }
    </style>
</asp:Content>