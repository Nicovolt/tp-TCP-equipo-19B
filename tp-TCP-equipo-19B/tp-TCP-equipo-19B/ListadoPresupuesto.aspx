<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoPresupuesto.aspx.cs" Inherits="tp_TCP_equipo_19B.Formulario_web16" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">


 <h2> Presupuesto</h2>
    
<asp:Repeater ID="RepeaterPresupuestos" runat="server">
    <ItemTemplate>
        <div class="card" style="margin-bottom: 15px; padding: 10px; border: 1px solid #ddd;">
            <h4>Presupuesto</h4>
            <p><strong>Cliente:</strong> <%# Eval("Cliente.Nombre") %> <%# Eval("Cliente.Apellido") %></p>

            <p><strong>Producto:</strong> <%# Eval("Detalles[0].Producto.Nombre") %></p>
            <p><strong>Total:</strong> <%# Eval("Total", "{0:C}") %></p>
            <p><strong>Forma de Pago:</strong> <%# Eval("FormaPago.Nombre") %></p>
            <p><strong>Fecha de Creación:</strong> <%# Eval("FechaCreacion", "{0:dd/MM/yyyy}") %></p>
        </div>
    </ItemTemplate>
</asp:Repeater>






</asp:Content>
