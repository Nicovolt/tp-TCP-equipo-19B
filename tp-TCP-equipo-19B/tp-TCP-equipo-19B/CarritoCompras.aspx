﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarritoCompras.aspx.cs" Inherits="tp_TCP_equipo_19B.Formulario_web14" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <style>
        .titulo-catalogo {
            text-align: center;
            font-size: 2rem;
            color: #000000;
            margin-top: 15px;
            margin-bottom: 15px;
            text-shadow: 2px 2px 2px rgba(0, 0, 0, 0.2);
        }

        .carrito-footer {
            display: flex;
            justify-content: space-between;
            margin-top: 20px;
            padding: 10px;
            border-top: 1px solid #ddd;
        }

        .carrito-footer .total {
            font-size: 1.5rem;
            font-weight: bold;
        }

        .error-message {
            color: red;
            font-size: 1rem;
            margin-top: 10px;
        }

        .carousel-item img {
            object-fit: contain;
            max-height: 200px;
            width: auto;
        }
        .carrito-container {
                display: flex;
                flex-wrap: wrap;
                justify-content: space-around; 
                gap: 20px; 
        }

        .producto-card {
            width: 250px;  
            background-color: #f9f9f9;  
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 8px;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            text-align: center;
        }

        .producto-card img {
            width: 100%;
            height: auto;
            border-radius: 5px;
        }

        .producto-card h5 {
            font-size: 1.2rem;
            margin-top: 10px;
        }

        .producto-card .precio {
            font-weight: bold;
            color: #28a745;
            margin-top: 5px;
        }

    </style>

    <h1 class="titulo-catalogo">Carrito de compras</h1>

    <asp:UpdatePanel ID="updatePanelCarrito" runat="server">
        <ContentTemplate>
            <div class="row">
                <asp:Repeater ID="repeaterCarrito" runat="server">
    <ItemTemplate>
        <div class="col-md-4 col-sm-6 mb-4"> 
            <div class="card">
                <div id="carousel<%# Eval("Id_producto") %>" class="carousel slide" data-bs-ride="carousel">
                    <div class="carousel-inner">
                        <asp:Repeater ID="repImagenes" runat="server" DataSource='<%# Eval("ListaImagenes") %>'>
                            <ItemTemplate>
                                <div class="carousel-item <%# Container.ItemIndex == 0 ? "active" : "" %>">
                                    <img src='<%# Eval("ImagenUrl") %>' class="d-block w-100 card-img-top" alt="Imagen del artículo">
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <button class="carousel-control-prev" type="button" data-bs-target="#carousel<%# Eval("Id_producto") %>" data-bs-slide="prev">
                        <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Previous</span>
                    </button>
                    <button class="carousel-control-next" type="button" data-bs-target="#carousel<%# Eval("Id_producto") %>" data-bs-slide="next">
                        <span class="carousel-control-next-icon" aria-hidden="true"></span>
                        <span class="visually-hidden">Next</span>
                    </button>
                </div>
                <div class="card-body">
                    <h5 class="card-title"><%# Eval("Nombre") %></h5>
                    <p class="card-text">Precio: $<%# Eval("Precio") %></p>

                    <div class="d-flex justify-content-between">
                        <div class="d-flex">
                            <asp:Button ID="btnDisminuirCantidad" CssClass="btn btn-info" runat="server" Text="-" OnClick="btnDisminuirCantidad_Click" CommandArgument='<%# Eval("Id_producto") %>' CommandName="disminuirCantidad" />
                            <asp:Label ID="lblCantidad" runat="server" Text='<%# Eval("Cantidad") %>' class="mx-2"></asp:Label>
                            <asp:Button ID="btnAumentarCantidad" CssClass="btn btn-info" runat="server" Text="+" OnClick="btnAumentarCantidad_Click" CommandArgument='<%# Eval("Id_producto") %>' CommandName="aumentarCantidad" />
                        </div>
                        <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-danger" Text="Quitar" OnClick="btnEliminar_Click" CommandArgument='<%# Eval("Id_producto") %>' CommandName="idArticulo" />
                    </div>
                </div>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>

            </div>

            <div class="carrito-footer">
                <div class="total">
                    Total: $  
                    <asp:Label ID="lblPrecioTotal" runat="server" Text="0"></asp:Label>
                </div>
                <asp:Button ID="btnComprar" OnClick="btnComprar_Click" runat="server" Text="Comprar" CssClass="btn btn-success" />
            </div>

            <asp:Label ID="lblError" runat="server" CssClass="error-message" Text="" Visible="false"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
