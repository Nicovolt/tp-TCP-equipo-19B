<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MarcaCategoria.aspx.cs" Inherits="tp_TCP_equipo_19B.Formulario_web11" %>

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
            margin-bottom: 10px;
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
        <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanelAgregar" runat="server">
            <ContentTemplate>
                <div class="card-custom">
                    <div class="card-header-custom">
                        <h4>Agregar Nueva Marca</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-contenedor">
                            <div class="mb-3">
                                <label for="formGroupExampleInput" class="form-label">Nombre Marca</label>
                                <asp:TextBox runat="server" type="text" class="form-control" id="inpNombreMar" placeholder="Nombre de marca"/>
                            </div>

                            <asp:Button Text="Agregar" runat="server" OnClick="Agregar" CssClass="btn-custom btn-success-custom" />
                            <asp:Label runat="server" ID="lblError" CssClass="label-error" />
                            <asp:Label ID="lblMensajeError" runat="server" CssClass="label-error" Visible="false"></asp:Label>

                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>


        <asp:UpdatePanel ID="UpdatePanelModificarEliminar" runat="server">
            <ContentTemplate>
                <div class="card-custom">
                    <div class="card-header-custom">
                        <h4>Modificar o Eliminar Marca</h4>
                    </div>
                    <div class="card-body">
                        <div class="form-contenedor">
                            <div class="mb-3">
                                <label for="ddlMarcape" class="form-label">Marca</label>
                                <asp:DropDownList runat="server" ID="ddlMarcape" CssClass="form-control">
                                </asp:DropDownList>
                            </div>

                            <div class="mb-3">
                                <label for="formGroupExampleInput" class="form-label">Nuevo Nombre Marca</label>
                                <asp:TextBox runat="server" type="text" class="form-control" ID="inpNombreMarcaNueva" placeholder="Nuevo nombre de marca" />
                            </div>
                            

                            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" CssClass="label-error" />
                            <asp:Button Text="Modificar" runat="server" OnClick="Modificar" CssClass="btn-custom btn-warning-custom" 
                                OnClientClick="return confirm('¿Estás seguro de que deseas modificar esta marca?');" />
                            <asp:Button Text="Eliminar" runat="server" OnClick="Eliminar" CssClass="btn-custom btn-danger-custom" 
                                OnClientClick="return confirm('¿Estás seguro de que deseas eliminar esta marca?');" />


                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </section>

</asp:Content>
