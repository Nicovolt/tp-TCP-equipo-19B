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
        position: relative;
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
        filter: invert(0);
    }

    @media (max-width: 768px) {
        .card-img-top, .carousel-inner {
            height: 300px;
        }
    }

    .btn-modificar:hover {
        background-color: #0056b3;
    }

   .modern-dropdown {
    width: 100%;
    max-width: 400px;
    padding: 10px;
    border: 2px solid #ccc;
    border-radius: 8px;
    font-size: 16px;
    color: #555;
    background: #f9f9f9;
    transition: border-color 0.3s ease, box-shadow 0.3s ease;
    position: relative;
    appearance: none; /* Ocultar flecha predeterminada */
    -webkit-appearance: none;
    -moz-appearance: none;
}

/* Cambios al hacer hover */
.modern-dropdown:hover {
    border-color: #4CAF50;
    box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

/* Enfoque (focus) */
.modern-dropdown:focus {
    outline: none;
    border-color: #2196F3;
    box-shadow: 0 0 10px rgba(33, 150, 243, 0.5);
}

/* Personalizar categorías y marcas */
/* Estilo base para todos los dropdowns */
.modern-dropdown {
    width: 100%;
    max-width: 100%;
    padding: 10px;
    font-size: 16px;
    font-weight: bold;
    color: #333;
    background: #f9f9f9; /* Fondo gris claro */
    border: 2px solid #ccc; /* Borde gris claro */
    border-radius: 8px; /* Esquinas redondeadas */
    box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1); /* Sombra ligera */
    appearance: none; /* Elimina estilo predeterminado */
    -webkit-appearance: none; /* Safari */
    -moz-appearance: none; /* Firefox */
    position: relative;
    transition: all 0.3s ease; /* Suavidad en hover/focus */
    cursor: pointer;
}

/* Flecha personalizada para los dropdowns */
.modern-dropdown::after {
    content: '▼';
    position: absolute;
    right: 15px;
    top: 50%;
    transform: translateY(-50%);
    pointer-events: none;
    font-size: 14px;
    color: #888;
}

/* Efecto al pasar el mouse */
.modern-dropdown:hover {
    border-color: #4CAF50; /* Borde verde */
    box-shadow: 0 6px 10px rgba(0, 0, 0, 0.2); /* Más sombra */
    background: #f1f1f1; /* Fondo más claro */
}

/* Efecto al enfocar (focus) */
.modern-dropdown:focus {
    outline: none;
    border-color: #2196F3; /* Borde azul */
    box-shadow: 0 0 8px rgba(33, 150, 243, 0.5); /* Brillo azul */
}

/* Etiqueta de Categorías */
#ddlCategoriasLabel {
    font-size: 18px;
    color: #FFA500; /* Naranja */
    font-weight: bold;
}

/* Etiqueta de Marcas */
#ddlMarcasLabel {
    font-size: 18px;
    color: #6A5ACD; /* Azul violeta */
    font-weight: bold;
}

/* Responsivo: Ajustes para dispositivos pequeños */
@media (max-width: 768px) {
    .modern-dropdown {
        font-size: 14px;
        padding: 8px;
    }

    #ddlCategoriasLabel, #ddlMarcasLabel {
        font-size: 16px;
    }
}


        

    </style>

    <div class="container">
        <h3>PRODUCTOS</h3>

     <div class="row mb-4">
        <div class="col-md-4">
            <div class="dropdown-container">
                <div class="dropdown-item">
                    <label id="ddlCategoriasLabel" for="ddlCategorias">Categorías</label>
                    <asp:DropDownList ID="ddlCategorias" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged" CssClass="modern-dropdown">
                        <asp:ListItem Text="Todas las categorías" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="dropdown-item">
                    <label id="ddlMarcasLabel" for="ddlMarcas">Marcas</label>
                    <asp:DropDownList ID="ddlMarcas" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMarcas_SelectedIndexChanged" CssClass="modern-dropdown">
                        <asp:ListItem Text="Todas las marcas" Value="0"></asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>
    </div>



        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row row-cols-1 row-cols-md-3 g-4">
                    <asp:Repeater ID="repProductos" runat="server" OnItemCommand="repeaterProducto_ItemCommand">
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
                                         <asp:Button ID="btnCarrito" runat="server" CommandName="AgregarAlCarrito" CommandArgument='<%# Eval("Id_producto") %>' Text="Agregar al carrito" CssClass="btn btn-success" />
                                         <asp:Button ID="btnDetalle" runat="server" CommandName="VerDetalle" CommandArgument='<%# Eval("Id_producto") %>' Text="Ver detalle" CssClass="btn btn-primary" />
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
