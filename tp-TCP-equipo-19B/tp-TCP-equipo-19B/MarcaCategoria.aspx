<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MarcaCategoria.aspx.cs" Inherits="tp_TCP_equipo_19B.Formulario_web11" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">


<section class="contenedor">
    <div class="form-contenedor">
        <div class="mb-3">
            <label for="formGroupExampleInput" class="form-label">Nombre Marca</label>
            <asp:TextBox runat="server" type="text" class="form-control" id="inpNombreMar" placeholder="Nombre de marca"/>
        </div>
     
       
        <asp:Button Text="Agregar" runat="server" onclick="Agregar"/>  
        


        <div class="mb-3">
    <label for="ddlMarcape" class="form-label">Marca</label>
    <asp:DropDownList runat="server" ID="ddlMarcape" CssClass="form-select">
        <asp:ListItem Text="Seleccione la Marca" Value="" />
    </asp:DropDownList>
       </div>

        <div class="mb-3">
    <label for="formGroupExampleInput" class="form-label">Nuevo Nombre Marca</label>
    <asp:TextBox runat="server" type="text" class="form-control" id="inpNombreMarcaNueva" placeholder="Nombre de marca"/>
</div>


        <asp:Button text="Modificar" runat="server" onclick="Modificar"/>
        <asp:Label runat="server" ID="lblError" CssClass="text-danger" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    </div>
</section>
</asp:Content>