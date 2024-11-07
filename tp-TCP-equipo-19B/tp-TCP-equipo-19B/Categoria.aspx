<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categoria.aspx.cs" Inherits="tp_TCP_equipo_19B.Formulario_web12" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <section class="contenedor">
    <div class="form-contenedor">
        <div class="mb-3">
            <label for="formGroupExampleInput" class="form-label">Nombre Categoria</label>
            <asp:TextBox runat="server" type="text" class="form-control" id="inpCat" placeholder="Nombre de categoria"/>
        </div>


        <asp:Button Text="Agregar" runat="server" Onclick="Agregar" />



        <div class="mb-3">
            <label for="ddlCategoria" class="form-label">categoria</label>
            <asp:DropDownList runat="server" ID="ddlCategoria" CssClass="form-select">
            </asp:DropDownList>
        </div>

        <div class="mb-3">
            <label for="formGroupExampleInput" class="form-label">Nuevo Nombre Categoria</label>
            <asp:TextBox runat="server" type="text" class="form-control" ID="inpNombreCategoriaNueva" placeholder="Nombre de categoria" />
        </div>


        <asp:Button text="Modificar" runat="server" Onclick="Modificar"/>
        <asp:Button Text="eliminar" runat="server" Onclick ="Eliminar"/>
        <asp:Label runat="server" ID="lblError" CssClass="text-danger" />
        <asp:Label ID="lblMensajeError" runat="server" ForeColor="Red" Visible="false"></asp:Label>

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>



 

    </div>

</section>

</asp:Content>
