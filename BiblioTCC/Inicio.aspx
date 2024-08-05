<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="BiblioTCC.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Assets/inicio.css" />
    <div class="container-all">
        <div class="text-intro">
            <h1>Seja bem-vindo ao BiblioTech</h1>
            <p>
                Acesse aqui opções administrativas básicas de sua
            biblioteca ou o menu superior para opções avançadas
            </p>
        </div>
        <div class="container-all-inside">
            <div class="cards-times">
                <div class="times" id="cadastro">
                    <img class="logo-time" src="assets/img/book.png" />
                    <p class="text-p">Cadastrar Livros</p>

                </div>
                <div class="times" id="emprestimo">
                    <img class="logo-time" src="assets/img/person.png" />
                    <p class="text-p">Cadastrar Emprestimo</p>
                </div>
            </div>
        </div>
    </div>
    <script src="Assets/js/inicio.js"></script>
</asp:Content>
