<%@ Page Title="Gestión de Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="tp_TCP_equipo_19B.GestionProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="container mt-4">
        <div class="row justify-content-center">
            <!-- Columna principal -->
            <div class="col-lg-8 col-md-10 col-sm-12">
                <div class="card shadow">
                    <div class="card-header bg-primary text-white">
                        <h3 class="mb-0">
                            <asp:Literal ID="ltlTitulo" runat="server" /></h3>
                    </div>
                    <div class="card-body">
                        <!-- Información básica -->
                        <div class="row mb-4">
                            <div class="col-md-6">
                                <div class="form-group mb-3">
                                    <label for="txtNombre" class="form-label">Nombre del Producto</label>
                                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingrese el nombre" />
                                    <asp:RequiredFieldValidator ID="rfvNombre" runat="server"
                                        ControlToValidate="txtNombre"
                                        ErrorMessage="El nombre es requerido"
                                        CssClass="text-danger" />
                                </div>

                                <div class="form-group mb-3">
                                    <label for="ddlCategoria" class="form-label">Categoria</label>
                                    <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-select">
                                        <asp:ListItem Text="Seleccione una categoria" Value="" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvCategoria" runat="server"
                                        ControlToValidate="ddlCategoria"
                                        ErrorMessage="Seleccione una categoria"
                                        CssClass="text-danger" />
                                </div>

                                <div class="form-group mb-3">
                                    <label for="txtPrecio" class="form-label">Precio</label>
                                    <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control"
                                        TextMode="Number" step="0.01" placeholder="0.00" />
                                    <asp:RequiredFieldValidator ID="rfvPrecio" runat="server"
                                        ControlToValidate="txtPrecio"
                                        ErrorMessage="El precio es requerido"
                                        CssClass="text-danger" />
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group mb-3">
                                    <label for="txtDescripcion" class="form-label">Descripcion</label>
                                    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control"
                                        TextMode="MultiLine" Rows="3" placeholder="Descripcion del producto" />
                                </div>

                                <div class="form-group mb-3">
                                    <label for="ddlMarca" class="form-label">Marca</label>
                                    <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-select">
                                        <asp:ListItem Text="Seleccione una marca" Value="" />
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfvMarca" runat="server"
                                        ControlToValidate="ddlMarca"
                                        ErrorMessage="Seleccione una marca"
                                        CssClass="text-danger" />
                                </div>

                                <div class="form-group mb-3">
                                    <label for="txtStock" class="form-label">Stock</label>
                                    <asp:TextBox ID="txtStock" runat="server" CssClass="form-control"
                                        TextMode="Number" placeholder="0" />
                                    <asp:RequiredFieldValidator ID="rfvStock" runat="server"
                                        ControlToValidate="txtStock"
                                        ErrorMessage="El stock es requerido"
                                        CssClass="text-danger" />
                                </div>
                            </div>
                        </div>

                        <!-- Sección de imágenes -->
                        <div class="row">
                            <div class="col-12">
                                <h4 class="mb-3">Imagenes del Producto</h4>
                                <div class="image-manager mb-3">
                                    <div class="input-group">
                                        <asp:TextBox ID="txtNuevaImagen" runat="server"
                                            CssClass="form-control"
                                            placeholder="URL de la imagen" />
                                        <button type="button" class="btn btn-secondary" onclick="previewImage(); return false;">
                                            <i class="fas fa-eye"></i>Preview
               
                                        </button>
                                        <asp:Button ID="btnAgregarImagen" runat="server"
                                            Text="+"
                                            CssClass="btn btn-primary"
                                            OnClick="btnAgregarImagen_Click"
                                            CausesValidation="false" />
                                    </div>
                                    <!-- Preview de imagen -->
                                    <div id="imagePreview" class="mt-2 d-none">
                                        <img id="previewImg" src="" alt="Preview" class="img-fluid" style="max-height: 200px;" />
                                    </div>
                                </div>

                                <asp:Panel ID="pnlImagenes" runat="server" CssClass="image-list">
                                </asp:Panel>
                            </div>
                        </div>

                        <!-- Botones de acción -->
                        <div class="row mt-4">
                            <div class="col-12 d-flex justify-content-end gap-2">
                                <asp:Button ID="btnCancelar" runat="server"
                                    Text="Cancelar"
                                    CssClass="btn btn-secondary"
                                    OnClick="btnCancelar_Click"
                                    CausesValidation="false" />

                                <asp:Button ID="btnGuardar" runat="server"
                                    Text="Guardar"
                                    CssClass="btn btn-primary"
                                    OnClick="btnGuardar_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <style>
        .image-manager {
            background: #f8f9fa;
            padding: 1rem;
            border-radius: 8px;
        }

        .image-list {
            max-height: 300px;
            overflow-y: auto;
        }

        .image-item {
            display: flex;
            align-items: center;
            gap: 10px;
            margin-bottom: 10px;
            padding: 8px;
            background: #fff;
            border: 1px solid #dee2e6;
            border-radius: 4px;
        }

        .image-item .form-control {
            flex: 1;
        }

        .image-item .btn-remove {
            padding: 0.375rem 0.75rem;
        }
    </style>
    <script type="text/javascript">
        function previewImage() {
            var url = document.getElementById('<%= txtNuevaImagen.ClientID %>').value;
            var preview = document.getElementById('imagePreview');
            var img = document.getElementById('previewImg');

            if (url) {
                img.src = url;
                preview.classList.remove('d-none');

                img.onerror = function () {
                    preview.classList.add('d-none');
                    alert('No se pudo cargar la imagen. Verifique la URL.');
                };

                img.onload = function () {
                    preview.classList.remove('d-none');
                };
            } else {
                preview.classList.add('d-none');
            }
        }
</script>
</asp:Content>
