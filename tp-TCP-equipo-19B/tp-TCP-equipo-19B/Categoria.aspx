<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categoria.aspx.cs" Inherits="tp_TCP_equipo_19B.Formulario_web12" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .card-custom {
            background-color: #333;
            color: white;
            border-radius: 10px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            max-width: 600px;
            margin: 30px auto;
            padding: 20px;
        }

        .card-header-custom {
            background-color: #444;
            text-align: center;
            padding: 10px;
            border-radius: 8px 8px 0 0;
        }

        .form-label {
            color: white;
            text-align: center;
            font-size: 18px;
            margin-bottom: 10px;
            margin-top: 10px;
            display: block;
        }

        .form-control {
            border-radius: 5px;
            padding: 15px;
            border: 1px solid #555;
            background-color: #444;
            color: white;
            font-size: 16px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.3);
            width: 100%;
        }

        .form-control:focus {
            border-color: #007bff;
            background-color: #555;
            box-shadow: 0 4px 10px rgba(0, 123, 255, 0.6);
        }

        .btn-custom {
            padding: 10px 15px;
            border-radius: 5px;
            margin-bottom: 10px;
            width: 100%;
        }

        .btn-success-custom {
            background-color: #28a745;
            border: none;
        }

        .btn-warning-custom {
            background-color: #ffc107;
            border: none;
        }

        .btn-danger-custom {
            background-color: #dc3545;
            border: none;
        }

        .btn-custom:hover {
            opacity: 0.8;
        }

        .label-error {
            color: red;
            font-weight: bold;
            text-align: center;
            display: block;
            margin-top: 10px;
        }

        .label-success {
            color: green;
            font-weight: bold;
            text-align: center;
            display: block;
            margin-top: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <section class="contenedor">
        <div class="card-custom">
            <div class="card-header-custom">
                <h4>Agregar Nueva Categoría</h4>
            </div>
            <div class="card-body">
                <div class="form-contenedor">
                    <div class="mb-3">
                        <label for="formGroupExampleInput" class="form-label">Nombre Categoria</label>
                        <asp:TextBox runat="server" type="text" class="form-control" id="inpCat" placeholder="Nombre de categoria" />
                    </div>

                    <asp:Button Text="Agregar" runat="server" Onclick="Agregar" CssClass="btn-custom btn-success-custom" />

                    <asp:Label runat="server" ID="lblError" CssClass="label-error" />
                    <asp:Label ID="lblMensajeError" runat="server" CssClass="label-error" Visible="false"></asp:Label>
                </div>
            </div>
        </div>

        <div class="card-custom">
            <div class="card-header-custom">
                <h4>Modificar o Eliminar Categoría</h4>
            </div>
            <div class="card-body">
                <div class="form-contenedor">
                    <div class="mb-3">
                        <label for="ddlCategoria" class="form-label">Categoria</label>
                        <asp:DropDownList runat="server" ID="ddlCategoria" CssClass="form-control">
                        </asp:DropDownList>
                    </div>

                    <div class="mb-3">
                        <label for="formGroupExampleInput" class="form-label">Nuevo Nombre Categoria</label>
                        <asp:TextBox runat="server" type="text" class="form-control" ID="inpNombreCategoriaNueva" placeholder="Nuevo nombre de categoria" />
                    </div>

                    <asp:Button Text="Modificar" runat="server" Onclick="Modificar" CssClass="btn-custom btn-warning-custom" />
                    <asp:Button Text="Eliminar" runat="server" Onclick="Eliminar" CssClass="btn-custom btn-danger-custom" />
                </div>
            </div>
        </div>
    </section>

</asp:Content>
