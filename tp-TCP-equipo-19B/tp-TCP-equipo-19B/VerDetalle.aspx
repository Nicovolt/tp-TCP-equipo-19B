<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VerDetalle.aspx.cs" Inherits="tp_TCP_equipo_19B.Formulario_web13" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
<style>
   /* Estilos para los botones y diseño */
</style>

<asp:UpdatePanel ID="upProductoDetalle" runat="server">
    <ContentTemplate>
        <asp:GridView runat="server" ID="dgvProductoDetalle"></asp:GridView>
        <div class="container mt-5">
            <div class="row">
                <div class="col-md-6" style="display: flex; flex-direction: column; justify-content: space-around; color: #000000">
                    <h2 style="text-align: center;">Detalle de producto</h2>
                    <hr />
                    <div style="display: flex; flex-direction: column;">
                        <span class="texto-secundario">Nombre:</span>
                        <asp:Label ID="lblNombreArticulo" runat="server" Text="" CssClass="font-weight-bold"></asp:Label><br />
                        <span class="texto-secundario">Descripcion:</span>
                        <asp:Label ID="lblDescripcionArticulo" runat="server" Text=""></asp:Label><br />
                        <span class="texto-secundario">Categoria: </span>
                        <asp:Label ID="lblCategoriaArticulo" runat="server" Text=""></asp:Label><br />
                        <span class="texto-secundario">Marca: </span>
                        <asp:Label ID="lblMarcaArticulo" runat="server" Text=""></asp:Label><br />
                        <span class="texto-secundario">Precio: </span>
                        <asp:Label ID="lblPrecioArticulo" runat="server" Text="" CssClass="font-weight-bold"></asp:Label>
                        <hr />
                        <div>
                            <asp:HiddenField ID="hfProductoID" runat="server" />
                            <asp:Button ID="btnCarrito" runat="server" OnClick="btnCarrito_Click" Text="Agregar al carrito" CssClass="btn btn-success" />
                            <asp:Button ID="btnBorrar" runat="server" Text="Eliminar" CssClass="btn btn-danger" CommandName="idBorrar" OnClick="btnBorrar_Click" OnClientClick="return confirmDelete();" />
                            <asp:Button ID="btnModificar" runat="server" Text="Modificar" CommandName="idModificar" OnClick="btnModificar_Click" CssClass="btn btn-primary" />
                        </div>
                    </div>
                </div>
                <div class="col-md-6" style="height: 60vh;">
                    <div id="carouselExample" class="carousel slide" style="height: 100%;" data-bs-ride="carousel">
                        <div class="carousel-inner" style="height: 100%;">
                            <asp:Repeater ID="repeaterImagenes" runat="server">
                                <ItemTemplate>
                                    <div class='carousel-item<%# Container.ItemIndex == 0 ? " active" : "" %>' style="height: 200px;">
                                        <div class="imagen-carrusel">
                                            <asp:Image ID="imgArticulo" runat="server" ImageUrl='<%# Eval("UrlImagen") %>' CssClass="d-block w-100 artImagen" alt="No se pudo cargar la imagen" />
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                        <a class="carousel-control-prev" style="background-color: #123261;" href="#carouselExample" role="button" data-bs-slide="prev">
                            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Previous</span>
                        </a>
                        <a class="carousel-control-next" style="background-color: #123261;" href="#carouselExample" role="button" data-bs-slide="next">
                            <span class="carousel-control-next-icon" aria-hidden="true"></span>
                            <span class="visually-hidden">Next</span>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>

<script type="text/javascript">
    function confirmDelete() {
        return confirm('¿Estás seguro de que quieres eliminar este producto?');
    }
</script>
</asp:Content>
