<%@ Page Title="Listado de pedidos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoPresupuestoDetalle.aspx.cs" Inherits="tp_TCP_equipo_19B.ListadoPresupuestos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container py-5">
        <div class="row">
            <div class="col-lg-8">
                <div class="card mb-4 shadow-sm">
                    <div class="card-header bg-primary text-white d-flex justify-content-between align-items-center">
                        <h4 class="mb-0">Presupuesto #<asp:Literal ID="ltlNroPresupuesto" runat="server" /></h4>
                        <div class="d-flex align-items-center">
                            <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select me-2" AutoPostBack="true" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged">
                            </asp:DropDownList>
                            <span class="badge" runat="server" id="badgeEstado"></span>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="mb-4">
                            <p class="mb-1"><strong>Cliente:</strong> <asp:Literal ID="ltlCliente" runat="server" /></p>
                            <p class="mb-1"><strong>Fecha:</strong> <asp:Literal ID="ltlFecha" runat="server" /></p>
                            <p class="mb-1"><strong>Estado:</strong> <asp:Literal ID="ltlEstado" runat="server" /></p>
                            <p class="mb-0"><strong>Última actualización:</strong> <asp:Literal ID="ltlUltimaActualizacion" runat="server" /></p>
                        </div>

                        <h5 class="mb-3">Productos</h5>
                        <div class="list-group">
                            <asp:Repeater ID="rptProductos" runat="server">
                                <ItemTemplate>
                                    <div class="list-group-item">
                                        <%# Eval("NombreProducto") %> - Cantidad: <%# Eval("Cantidad") %>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>

                <asp:Button ID="btnVolver" runat="server" Text="Volver al Listado" 
                    CssClass="btn btn-secondary" OnClick="btnVolver_Click" />
            </div>

            <div class="col-lg-4">
                <div class="card shadow-sm">
                    <div class="card-header">
                        <h5 class="mb-0">Información de Pago</h5>
                    </div>
                    <div class="card-body">
                        <p class="mb-2"><strong>Método:</strong> <asp:Literal ID="ltlMetodoPago" runat="server" /></p>
                        <hr>
                        <div class="d-flex justify-content-between">
                            <span class="h5 mb-0">Total:</span>
                            <strong class="h5 mb-0">$<asp:Literal ID="ltlTotal" runat="server" /></strong>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
