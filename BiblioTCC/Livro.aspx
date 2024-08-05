<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Livro.aspx.cs" Inherits="BiblioTCC.Livro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Assets/livro.css" />

    <div class="btnExit">
         <asp:LinkButton runat="server" ID="btnVoltar" Text="Voltar" CssClass="pesquisarButton" Font-Underline="false" OnClick="btnVoltar_Click"></asp:LinkButton>
    </div>
    <div class="menu-container">
        <div class="menu-container-div">
            <div class="container-home">
                <section class="section section-left">
                    <div class="section-left-items">
                        <h1 class="section-left-items-h1" runat="server" id="tituloLivro"></h1>
                        <br />
                        <h3 class="section-left-items-h3" id="autorLivroo" runat="server"></h3>
                        <div class="section-left-items-div">
                          <p runat="server" id="sinopseLivroo"></p>
                        </div>
                        <div class="section-left-items-div2">
                        </div>
                    </div>
                </section>
                <section class="section section-right">
                    <img id="capaLivro" runat="server" src="assets/img/pesquisa/image%2068.png" />
                    <div>
                    <asp:Button ID="btnUpload" runat="server" Text="Editar Capa" OnClick="btnUpload_Click" Visible="true"  />
                    <asp:FileUpload ID="fileUpload" runat="server" Visible="false" />
                    <asp:Button ID="btnSalvar" runat="server" Text="Salvar" OnClick="btnSalvar_Click" Visible="false" />
                    </div>
                    
                </section>
            </div>
        </div>
    </div>



   <script src="Assets/js/livro.js"></script>
    <asp:label runat="server" id="codLivroLabel" visible="false"></asp:label>

</asp:Content>
