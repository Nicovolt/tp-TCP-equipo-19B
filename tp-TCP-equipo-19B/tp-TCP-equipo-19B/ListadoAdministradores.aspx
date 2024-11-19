<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListadoAdministradores.aspx.cs" Inherits="tp_TCP_equipo_19B.WebForm2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="container mt-4">
        <h2>Administración de Usuarios</h2>

        <!-- Filtros y Búsqueda -->
        <div class="row mb-4">
            <div class="col-md-4">
                <div class="form-group">
                    <label for="ddlFiltroAdmin">Filtrar por tipo:</label>
                    <asp:DropDownList ID="ddlFiltroAdmin" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroAdmin_SelectedIndexChanged">
                        <asp:ListItem Text="Todos" Value="todos" />
                        <asp:ListItem Text="Administradores" Value="true" />
                        <asp:ListItem Text="No Administradores" Value="false" />
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-md-8">
                <div class="input-group">
                    <asp:TextBox ID="txtBuscar" runat="server" CssClass="form-control" placeholder="Buscar por nombre o apellido..." />
                    <asp:Button ID="btnBuscar" runat="server" CssClass="btn btn-primary" Text="Buscar" OnClick="btnBuscar_Click" />
                    <asp:Button ID="btnLimpiar" runat="server" CssClass="btn btn-secondary" Text="Limpiar" OnClick="btnLimpiar_Click" />
                </div>
            </div>
        </div>

        <!-- Grilla de Usuarios -->
        <asp:UpdatePanel ID="upUsuarios" runat="server">
            <ContentTemplate>
                <div class="table-responsive">
                    <asp:GridView ID="gvUsuarios" runat="server" 
                        CssClass="table table-striped table-hover" 
                        AutoGenerateColumns="false"
                        DataKeyNames="IdUsuario"
                        OnRowCommand="gvUsuarios_RowCommand"
                        OnRowDataBound="gvUsuarios_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="IdUsuario" HeaderText="ID" />
                            <asp:BoundField DataField="NombreCompleto" HeaderText="Nombre Completo" />
                            <asp:BoundField DataField="Mail" HeaderText="Email" />
                            <asp:TemplateField HeaderText="Administrador">
                                <ItemTemplate>
                                    <div class="form-check">
                                        <asp:CheckBox ID="chkAdmin" runat="server" 
                                            Checked='<%# Eval("Admin") %>'
                                            AutoPostBack="true"
                                            OnCheckedChanged="chkAdmin_CheckedChanged"/>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <asp:Button ID="btnGuardar" runat="server" 
                                        Text="Guardar" 
                                        CommandName="GuardarCambios" 
                                        CommandArgument='<%# Eval("IdUsuario") %>'
                                        CssClass="btn btn-success btn-sm"
                                        Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <!-- Panel de Mensajes -->
                <asp:Panel ID="pnlMensaje" runat="server" Visible="false" CssClass="alert alert-dismissible fade show mt-3">
                    <asp:Label ID="lblMensaje" runat="server" />
                    <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                </asp:Panel>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
