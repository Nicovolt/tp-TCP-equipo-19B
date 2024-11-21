<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Banner.aspx.cs" Inherits="tp_TCP_equipo_19B.Formulario_web15" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">


     <h4 class="mb-3">Imagenes del Banner</h4>

     <div class="input-group">
         <asp:TextBox ID="txtNuevaImagen" runat="server"
             CssClass="form-control"
             placeholder="URL de la imagen" />
       
  
         <asp:Button ID="btnAgregarImagen" runat="server"   CssClass="btn btn-primary"  OnClick="btnAgregarImagen_Click"   CausesValidation="false"/> 
    
             
     </div>


</asp:Content>
