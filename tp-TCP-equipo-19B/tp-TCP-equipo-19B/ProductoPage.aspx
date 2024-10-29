<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductoPage.aspx.cs" Inherits="tp_TCP_equipo_19B.ProductoPage" %>


    <style>
        .contenedor {
            width: 100%;
            display: flex;
            align-items: center;
            justify-content: center;
            position: relative;
        }

            .contenedor > hi {
                align-content: center;
            }

        .form-contenedor {
            width: 100%;
            max-width: 450px;
            background: linear-gradient(to top, #30cfd0 0%, #330867 100%);
            border-radius: 15px;
            color: white;
            padding: 50px 60px 70px;
        }
    </style>
    <div>

        <section class="contenedor">
            <div class="form-contenedor">
                <%if (Request.QueryString["id"] != null)
                    {%>
                <h1>Modificar articulo</h1>
               <% }else{%>
                    <h1>Nuevo articulo</h1>
               <% }%> 
                <div class="mb-3">
                    <label for="formGroupExampleInput" class="form-label">Nombre Producto</label>
                    <asp:TextBox runat="server" type="text" class="form-control" id="inpNombreArticulo" placeholder="Nombre del producto"/>
                </div>
                <div class="mb-3">
                    <label for="formGroupExampleInput2" class="form-label">Descripcion</label>
                    <asp:TextBox runat="server" type="text" class="form-control" id="inpDescripcion" placeholder="Descripcion del producto"/>
                </div>
                <div class="mb-3">
                    <label for="ddlCategoria" class="form-label">Categoria</label>
                    <asp:DropDownList runat="server" ID="ddlCategoria" CssClass="form-select">
                        <asp:ListItem Text="Seleccione la Categoria" Value="" />
                    </asp:DropDownList>
                </div>
                <div class="mb-3">
                    <label for="ddlMarca" class="form-label">Marca</label>
                    <asp:DropDownList runat="server" ID="ddlMarca" CssClass="form-select">
                        <asp:ListItem Text="Seleccione la Marca" Value="" />
                    </asp:DropDownList>
                </div>
                <div class="mb-3">
                    <label for="formGroupExampleInput2" class="form-label">Precio</label>
                    <asp:TextBox runat="server" type="text" class="form-control" id="inpPrecio" placeholder="Precio del producto"/>
                </div>
                <div class="mb-3">
                    <label for="formGroupExampleInput" class="form-label">Stock</label>
                    <asp:TextBox runat="server" type="text" class="form-control" id="inpStock" placeholder="Cantidad de producto"/>
                </div>
                
                <div class="mb-3">
                    <label for="formGroupExampleInput" class="form-label">Imagen</label>
                    <asp:TextBox runat="server" type="text" class="form-control" ID="inpImagen" placeholder="Url de la imagen" />
                </div>
                <div>
                   <asp:button text="Agregar" CssClass="btn btn-outline-light" runat="server" onclick=/>
                </div>
                <asp:Label runat="server" ID="lblError" CssClass="text-danger" />
            </div>
        </section>

    </div>
</asp:Content>

