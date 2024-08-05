<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BiblioTCC._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-aFq/bzH65dt+w6FI2ooMVUpc+21e0SRygnTpmBvdBgSdnuTN7QbdgL+OapgHtvPp" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha2/dist/js/bootstrap.bundle.min.js"
        integrity="sha384-qKXV1j0HvMUeCBQ+QVp7JcfGl760yU08IQ+GpUo5hlbpg51QRiuqHAJz8+BrxE/N" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="Assets/default.css" />

    <div class="body">
        <div class="container py-5 h-100">
            <div class="col mx-auto text-center">
                <img class="img-responsive" src="img/LogoBiblio.png" alt="Logotipo" height="200px" width="200px">
            </div>

            <div class="mb-3">
                <asp:Label class="form-label" runat="server">Usuário</asp:Label>
                <asp:TextBox ID="loginTextBox" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:Label class="form-label" runat="server">Senha</asp:Label>
                <asp:TextBox ID="senhaTextBox" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                <div class="text-center">
                    <asp:Button ID="btnEntrar" CssClass="btn btn-warning" runat="server" Text="Entrar" OnClick="btnEntrar_Click" />
                </div>
            </div>
        </div>
    </div>

    <div id="modal">
        <div>
            <h2 id="tituloModal"></h2>
            <p id="mensagemModal"></p>
        </div>
        <div class="modal-button">
            <a id="modal-button-close" onclick="FecharModal()">Fechar</a>
        </div>
    </div>

    <script src="Assets/js/default.js"></script>
</asp:Content>
