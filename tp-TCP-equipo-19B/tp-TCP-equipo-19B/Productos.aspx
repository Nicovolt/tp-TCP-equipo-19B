<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="tp_TCP_equipo_19B.Formulario_web1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
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
                  <div class="mb-3">
                      <label for="formGroupExampleInput" class="form-label">Nombre Producto</label>
                      <asp:TextBox runat="server" type="text" class="form-control" id="inpNombrePro" placeholder="Nombre del producto"/>
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
                      <label class="form-label">Imágenes del Producto</label>
                      <asp:Panel ID="pnlImagenes" runat="server">
                      </asp:Panel>
                      <!-- TextBox original para nueva imagen -->
                      <asp:TextBox runat="server" type="text" class="form-control mt-2" ID="inpImagen" placeholder="Url de imagen" />
                  </div>
                  <asp:Button Text="Agregar" runat="server" OnClick="AgregarPro" />
                  <asp:Button text="Modificar" runat="server" OnClick="ModificarPro"/>
                  <asp:Label runat="server" ID="lblError" CssClass="text-danger" />
                  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
              </div>
          </section>
  </div>




</asp:Content>
