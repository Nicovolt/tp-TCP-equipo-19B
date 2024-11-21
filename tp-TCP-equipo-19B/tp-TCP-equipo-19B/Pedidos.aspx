<%@ Page Title="Mis Pedidos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="tp_TCP_equipo_19B.Pedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container py-5">
        <h2 class="mb-4">Mis Pedidos</h2>

        <!-- Filtros -->
        <div class="row mb-4">
            <div class="col-md-4">
                <label class="form-label">Ordenar por</label>
                <asp:DropDownList ID="ddlOrden" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlOrden_SelectedIndexChanged">
                    <asp:ListItem Text="Más recientes" Value="fecha_desc" />
                    <asp:ListItem Text="Más antiguos" Value="fecha_asc" />
                    <asp:ListItem Text="Mayor monto" Value="monto_desc" />
                    <asp:ListItem Text="Menor monto" Value="monto_asc" />
                </asp:DropDownList>
            </div>
        </div>

        <!-- Listado de Pedidos -->
        <div class="row">
            <asp:Repeater ID="rptPedidos" runat="server">
                <ItemTemplate>
                    <div class="col-md-6 mb-4">
                        <div class="card h-100 shadow-sm">
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-center mb-3">
                                    <h5 class="card-title mb-0">Pedido #<%# Eval("Id") %></h5>
                                    <span class='badge <%# GetEstadoClass((short)Eval("IdEstado")) %>'>
                                        <%# Eval("Estado.Nombre") %>
                                    </span>
                                </div>

                                <div class="mb-3">
                                    <p class="card-text mb-1">
                                        <i class="fas fa-calendar me-2"></i>
                                        <%# Convert.ToDateTime(Eval("FechaCreacion")).ToString("dd/MM/yyyy HH:mm") %>
                                    </p>
                                    <p class="card-text mb-1">
                                        <i class="fas fa-money-bill me-2"></i>
                                        Total: $<%# Convert.ToDecimal(Eval("Total")).ToString("N2") %>
                                    </p>
                                </div>

                                <a href='PedidoDetalle.aspx?id=<%# Eval("Id") %>' class="btn btn-primary w-100">Ver Detalle
                                </a>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <!-- Mensaje si no hay pedidos -->
        <asp:Panel ID="pnlNoResults" runat="server" CssClass="text-center py-5" Visible="false">
            <i class="fas fa-shopping-bag fa-3x mb-3 text-muted"></i>
            <h4>No hay pedidos para mostrar</h4>
            <p class="text-muted">¡Empieza a comprar ahora!</p>
            <asp:HyperLink ID="lnkComprar" runat="server" NavigateUrl="~/Default.aspx" CssClass="btn btn-primary">
                Ir a la Tienda
            </asp:HyperLink>
        </asp:Panel>
    </div>

    <style>
        .badge {
            padding: 8px 12px;
            font-size: 0.875rem;
        }
        .badge-pending { background-color: #0dcaf0; color: #000; }  /* azul claro */
        .badge-success { background-color: #198754; color: #fff; }  /* verde */
        .badge-danger { background-color: #dc3545; color: #fff; }   /* rojo */
        .badge-dark { background-color: #212529; color: #fff; }     /* negro */
        .badge-info { background-color: #0dcaf0; color: #000; }     /* azul informativo */
        .badge-primary { background-color: #0d6efd; color: #fff; }  /* azul primario */
        .badge-purple { background-color: #6f42c1; color: #fff; }   /* violeta */
        .badge-secondary { background-color: #6c757d; color: #fff; } /* gris */
    </style>
</asp:Content>