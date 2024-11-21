
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

    .add-to-cart-btn {
        position: relative;
        transition: background-color 0.3s ease;
        overflow: hidden;
    }

    .add-to-cart-btn::after {
        content: "";
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        font-size: 14px;
        color: transparent; /* Inicialmente transparente */
        opacity: 0; /* No visible al inicio */
        transition: opacity 0.3s ease;
        pointer-events: none;
    }

    /* Estilo cuando el botón se hace clic */
    .add-to-cart-btn.agregado::after {
        content: "✓ Agregado!";
        color: white; /* Color blanco para el texto */
        opacity: 1; /* Hace el texto visible */
        z-index: 10; /* Asegura que el texto esté por encima del contenido original */
        animation: resetText 2s forwards; /* La animación durará 2 segundos */
    }

    /* Anima la transición de fondo y texto */
    .add-to-cart-btn.agregado {
        background-color: #218838; /* Verde más oscuro al agregar al carrito */
    }

    /* Animación para restaurar el texto original después de 2 segundos */
    @keyframes resetText {
        0% {
            opacity: 1;
        }
        90% {
            opacity: 1;
        }
        100% {
            opacity: 0; /* Restaura el texto a invisible */
        }
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


.modern-dropdown:hover {
    border-color: #2196F3; /* Borde verde */
    box-shadow: 0 6px 10px rgba(0, 0, 0, 0.2); 
    background: #f1f1f1; /* Fondo más claro */
}

/* Efecto al enfocar (focus) */
.modern-dropdown:focus {
    outline: none;
    border-color: #2196F3; 
    box-shadow: 0 0 8px rgba(33, 150, 243, 0.5); 
}

/* Etiqueta de Categorías */
#ddlCategoriasLabel {
    font-size: 18px;
    color: #333; 
    font-weight: bold;
}

/* Etiqueta de Marcas */
#ddlMarcasLabel {
    font-size: 18px;
    color: #333; 
    font-weight: bold;
}

   .separador {
    height: 1px;
    background-color: #ddd; 
    margin: 20px 0; 
    box-shadow: 0 1px 3px rgba(0, 0, 0, 0.1); 
    margin-top: 50px;
    margin-bottom: 20px;
}


    .search-input {
     flex: 1; 
     width: calc(100% - 50px); 
     border: none;
     outline: none;
     padding: 10px;
     font-size: 16px;
 }

 .search-button {
     background-color: #000000;
     color: white;
     border: none;
     padding: 10px 14.5px;
     border-radius: 0 20px 20px 0;
     cursor: pointer;
 }

 .search-icon {
     font-size: 20px;
 }
 .search-container {
     text-align: center; 
     margin-top: 50px; 
     margin-bottom: 50px;
     width: 80%; 
     max-width: 500px;
     border: 1px solid #ccc;
     border-radius: 20px;
     overflow: hidden;
     margin: 0 auto; 
 }
        

    </style>


    <div class="search-container">
          <asp:TextBox ID="searchTextBox" runat="server" CssClass="search-input" placeholder="Buscar..." onkeydown="enter(event)"></asp:TextBox>
          <asp:LinkButton ID="btnSearch" runat="server" CssClass="search-button" OnClick="btnSearch_Click" type="button">
           <i class="fas fa-search"></i>
          </asp:LinkButton>
    </div>

     <div class="row row-cols-1 row-cols-md-3 g-4">
        <asp:Literal ID="Resultados" runat="server"></asp:Literal>
    </div>
       <div class="separador"></div>

    <div class="container">
        <h3>PRODUCTOS</h3>

     <div class="row mb-4">
        <div class="col-md-4">
            <div class="dropdown-container">
               <div class="dropdown-item">
    <label id="ddlCategoriasLabel" for="ddlCategorias">Categorias</label>
  <asp:DropDownList ID="ddlCategorias" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCategorias_SelectedIndexChanged" CssClass="modern-dropdown">
    <asp:ListItem Text="Todas las categorias" Value="0" />
    
</asp:DropDownList>

<asp:DropDownList ID="ddlMarcas" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMarcas_SelectedIndexChanged" CssClass="modern-dropdown">
    <asp:ListItem Text="Todas las marcas" Value="0" />

</asp:DropDownList>
</div>





                <div class="dropdown-item">
                    <asp:Label  id="ddlPrecioLabel" for="ddlPrecio"  text="Ordenar por" runat="server" />
                    <asp:DropDownList runat="server" id="ddlPrecio" AutoPostBack="true" OnSelectedIndexChanged="ddlPrecio_SelectedIndexChanged" CssClass="modern-dropdown">
                        <asp:ListItem Text="Precio: Menor a Mayor" value="1"/>
                        <asp:ListItem Text="Precio: Mayor a Menor" value="2"/>
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
                                            <asp:Repeater ID="repImagenes" runat="server" DataSource='<%# GetImagenesOrDefault(Eval("ListaImagenes")) %>'>
                                                <ItemTemplate>
                                                    <div class='carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>'>
                                                        <img src='<%# Eval("ImagenUrl") %>' class="d-block w-100 card-img-top" alt="Imagen del artículo">
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </div>
                                        <asp:Panel runat="server" Visible='<%# (GetImagenesOrDefault(Eval("ListaImagenes"))).Count > 1 %>'>
                                            <button class="carousel-control-prev" type="button" data-bs-target='#carouselArticulo<%# Eval("Id_producto") %>' data-bs-slide="prev">
                                                <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                                                <span class="visually-hidden">Anterior</span>
                                            </button>
                                            <button class="carousel-control-next" type="button" data-bs-target='#carouselArticulo<%# Eval("Id_producto") %>' data-bs-slide="next">
                                                <span class="carousel-control-next-icon" aria-hidden="true"></span>
                                                <span class="visually-hidden">Siguiente</span>
                                            </button>
                                        </asp:Panel>
                                    </div>
                                    <div class="card-body">
                                        <h5 class="card-title"><%# Eval("Nombre") %></h5>
                                        <p class="card-text"><%# Eval("Descripcion") %></p>
                                        <p class="card-text"><strong>Precio: </strong>$<%# Eval("Precio") %></p>
                                        <div class="d-flex justify-content-between align-items-center mt-auto">
                                            <asp:Button ID="btnCarrito" runat="server" CommandName="AgregarAlCarrito" CommandArgument='<%# Eval("Id_producto") %>' Text="Agregar al carrito" CssClass="btn btn-success add-to-cart-btn" />
                                            <asp:Button ID="btnDetalle" runat="server" CommandName="VerDetalle" CommandArgument='<%# Eval("Id_producto") %>' Text="Ver detalle" CssClass="btn btn-primary" />

                                        </div>
                                    </div>

                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel> 
    </div>


 
    <script>
        function enter(event) {
            if (event.keyCode === 13 || event.which === 13) {
                event.preventDefault();
                document.getElementById('<%= btnSearch.ClientID %>').click();
            }
        }
    </script>

    <script type="text/javascript">
        function confirmDelete() {
            return confirm('¿Estás seguro de que quieres eliminar este producto?');
        }
    </script>

</asp:Content>


