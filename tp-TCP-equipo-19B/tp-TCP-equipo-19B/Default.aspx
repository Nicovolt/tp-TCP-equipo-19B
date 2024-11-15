<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="tp_TCP_equipo_19B._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <style>
        .card-img-top {
            width: 100%;
            height: 500px;
            object-fit: contain;
        }
        .card {
            height: 100%;
            overflow: hidden;
            position: relative; /* Necesario para el botón de borrar */
            margin-bottom: 0;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.5); 
            transition: transform 0.2s; 
        }
        .card:hover {
            transform: translateY(-2px); 
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
        .carousel-control-prev-icon,
        .carousel-control-next-icon {
            filter: invert(0); /* Cambia a negro */
        }
        @media (max-width: 768px) {
            .card-img-top, .carousel-inner {
                height: 300px;
            }
        }
        /* Estilos para el botón de borrar */
        .btn-borrar {
            position: absolute;
            top: 10px;
            left: 10px;
            background-color: #ff4d4d;
            color: white;
            border: none;
            padding: 5px 10px;
            border-radius: 5px;
            cursor: pointer;
            z-index: 10;
        }
        .btn-borrar:hover {
            background-color: #ff1a1a;
        }
        /* Estilos para el botón de modificar */
        .btn-modificar {
            position: absolute;
            bottom: 10px;
            right: 10px; /* Cambiado de left a right para moverlo a la esquina inferior derecha */
            background-color: #007bff;
            color: white;
            border: none;
            padding: 5px 10px;
            border-radius: 5px;
            cursor: pointer;
        }
        .btn-modificar:hover {
            background-color: #0056b3;
        }
    </style>

    <div class="container">
        <h3>PRODUCTOS</h3>

        <div class="row mb-4">
        <div class="col-md-4">
            <asp:DropDownList ID="ddlCategorias" runat="server" AutoPostBack="true" onselectedindexchanged="ddlCategorias_SelectedIndexChanged" CssClass="form-control">
                <asp:ListItem Text="Todas las categorías" Value="0"></asp:ListItem>
            </asp:DropDownList>
            <asp:DropDownList ID="ddlMarcas" runat="server" AutoPostBack="true" onselectedindexchanged="ddlMarcas_SelectedIndexChanged" CssClass="form-control">
    <asp:ListItem Text="Todas las marcas" Value="0"></asp:ListItem>
</asp:DropDownList>
        </div>
    </div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row row-cols-1 row-cols-md-3 g-4">
                    <asp:Repeater ID="repProductos" runat="server" OnItemCommand="repeaterProducto_ItemCommand">
                        <ItemTemplate>
                            <div class="col">
                                <div class="card">
                                    
                                    <asp:Button ID="btnBorrar" runat="server" Text="X" CssClass="btn-borrar" CommandArgument='<%# Eval("Id_producto") %>' CommandName="idBorrar" OnClientClick="return confirmDelete();"  Visible='<%# ((dominio.Cliente)Session["usuario"] != null && ((dominio.Cliente)Session["usuario"]).rol == true) %>'/>
                                    <asp:Button ID="btnModificar" runat="server" Text="Modificar" CommandArgument='<%# Eval("Id_producto") %>' CommandName="idModificar" CssClass="btn-modificar" Visible='<%# ((dominio.Cliente)Session["usuario"] != null && ((dominio.Cliente)Session["usuario"]).rol == true) %>'/>

                                    <asp:Button ID="btnDetalle" runat="server" CommandName="VerDetalle" CommandArgument='<%# Eval("Id_producto") %>' Text="Ver detalle" CssClass="btn btn-primary" />
                                    <asp:Button ID="btnCarrito" runat="server" CommandName="AgregarAlCarrito" CommandArgument='<%# Eval("Id_producto") %>' Text="Agregar al carrito" CssClass="btn btn-success" />

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
            </ContentTemplate>
        </asp:UpdatePanel> 
    </div>





    <script type="text/javascript">
        function confirmDelete() {
            return confirm('¿Estás seguro de que quieres eliminar este producto?');
        }
    </script>

</asp:Content>
