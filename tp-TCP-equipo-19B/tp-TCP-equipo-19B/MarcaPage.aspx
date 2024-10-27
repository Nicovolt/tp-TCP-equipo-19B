<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MarcaPage.aspx.cs" Inherits="tp_TCP_equipo_19B.MarcaPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
             <asp:TextBox ID="txtMarca" runat="server" CssClass="search-input" placeholder="Buscar..." onkeydown="enter(event)"></asp:TextBox> runat="server" />
             <asp:Button id="btnAgregar" Text="Agregar" runat="server" Onclick="btnAgregar_Click" />
             <asp:Button id="btnEliminar" Text="Eliminar" runat="server" Onclick="btnEliminar_Click" />
            <asp:Button id="btnModificar" Text="Modificar" runat="server" Onclick="btnModificar_Click" />
             <asp:TextBox ID="txtNuevaMarca" runat="server" CssClass="search-input" placeholder="Buscar..." onkeydown="enter(event)"></asp:TextBox> runat="server" />
        </div>
    </form>
</body>
</html>
