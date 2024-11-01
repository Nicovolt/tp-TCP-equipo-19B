<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tp_TCP_equipo_19B._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <style>
        .card-img-top {
            width: 100%;
            height: 500px;
            object-fit: contain;

        }
        .card {
            height: 100%;
            overflow: hidden;
        }
        .card-body {
            display: flex;
            flex-direction: column;
        }
        .card-text {
            flex-grow: 1;
        }
        .carousel-item {
            text-align: center;
        }
        .carousel-inner {
            height: 350px;
        }
        @media (max-width: 768px) {
            .card-img-top, .carousel-inner {
                height: 300px;
            }
        }
                .card {
            height: 100%;
            overflow: hidden;
            margin-bottom: 0; /* Eliminar margen inferior en la tarjeta */
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.5); 
            transition: transform 0.2s; 
        }

        .card:hover {
            transform: translateY(-2px); 
}
    </style>
  <div class="container">
    <h3>PRODUCTOS</h3>
    <div class="row row-cols-1 row-cols-md-3 g-4">
        <asp:Repeater ID="repProductosSorteo" runat="server">
            <ItemTemplate>
                <div class="col">
                    <div class="card">
                        <div id='carouselArticulo<%# Eval("Id_producto") %>' class="carousel slide" data-bs-ride="carousel">
                            <div class="carousel-inner">
                                <asp:Repeater ID="repImagenes" runat="server" DataSource='<%# Eval("ListaImagenes") %>'>
                                    <ItemTemplate>
                                        <div class='carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>'>
                                            <img src='<%# Eval("ImagenUrl") %>' class="d-block w-100 card-img-top" alt="Imagen del artículo">
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <button class="carousel-control-prev" type="button" data-bs-target='#carouselArticulo<%# Eval("Id_producto") %>' data-bs-slide="prev">
                                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                <span class="visually-hidden">Anterior</span>
                            </button>
                            <button class="carousel-control-next" type="button" data-bs-target='#carouselArticulo<%# Eval("Id_producto") %>' data-bs-slide="next">
                                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                <span class="visually-hidden">Siguiente</span>
                            </button>
                        </div>
                        <div class="card-body">
                            <h5 class="card-title"><%# Eval("Nombre") %></h5>
                            <p class="card-text"><%# Eval("Descripcion") %></p>
                            <p class="card-text"><strong>Precio: </strong>$<%# Eval("Precio") %></p> 
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>


    <asp:Repeater runat="server" ID="repProductos"   OnItemCommand="repeaterProducto_ItemCommand">
        <ItemTemplate>

               <tr>
<td><%# Eval("Id_producto") %></td>
<td><%# Eval("Nombre") %></td>
<td><%# Eval("Descripcion") %></td>
<td><%# Eval("Precio") %></td>
<td><%# Eval("Id_marca") %></td>
<td><%# Eval("Id_categoria") %></td>
<td><%# Eval("stock") %></td>
     
<td>
    <asp:Button ID="btnModificar" runat="server" Text="Modificar" CommandArgument='<%# Eval("Id_producto") %>' CommandName="idModificar" CssClass="btn btn-primary" /></td>
    </tr>
        </ItemTemplate>
    </asp:Repeater>

</asp:Content>
