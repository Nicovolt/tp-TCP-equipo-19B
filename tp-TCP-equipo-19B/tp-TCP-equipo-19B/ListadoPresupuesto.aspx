<%@ Page Title="Listado de Presupuestos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoPresupuesto.aspx.cs" Inherits="tp_TCP_equipo_19B.ListadoPresupuesto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
        <div class="container py-5">
        <h2 class="mb-4">Listado de Presupuestos</h2>

        <!-- Listado de Presupuestos -->
        <div class="row">
            <asp:Repeater ID="RepeaterPresupuestos" runat="server">
                <ItemTemplate>
                    <div class="col-md-6 mb-4">
                        <div class="card h-100 shadow-sm">
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-center mb-3">
                                    <h5 class="card-title mb-0">Presupuesto #<%# Eval("Id") %></h5>
                                    <span class='badge <%# GetEstadoClassFromCode(Eval("IdEstado")) %>'>
                                        <%# Eval("Estado.Nombre") %>
                                    </span>
                                </div>

                                <p class="card-text mb-2">
                                    <strong>Cliente:</strong> <%# Eval("Cliente.Nombre") %> <%# Eval("Cliente.Apellido") %>
                                </p>
                                <p class="card-text mb-2">
                                    <i class="fas fa-calendar me-2"></i><%# Convert.ToDateTime(Eval("FechaCreacion")).ToString("dd/MM/yyyy HH:mm") %>
                                </p>
                                <p class="card-text mb-2">
                                    <strong>Método de pago:</strong> <%# Eval("FormaPago.Nombre") %>
                                </p>
                                <p class="card-text mb-2">
                                    <i class="fas fa-money-bill me-2"></i>Total: $<%# Convert.ToDecimal(Eval("Total")).ToString("N2") %>
                                </p>

                                <a href='ListadoPresupuestoDetalle.aspx?id=<%# Eval("Id") %>' class="btn btn-primary w-100">Ver Detalle</a>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
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
