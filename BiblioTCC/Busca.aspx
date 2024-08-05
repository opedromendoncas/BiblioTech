<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Busca.aspx.cs" Inherits="BiblioTCC.Busca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Assets/busca.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css" />


    <div class="container-all">

         <div class="pesquisar-div">
                <br />
                <br />
                <div class="input-group">
                    <asp:DropDownList ID="genDropDownList" runat="server" CssClass="form-select" DataSourceID="genSqlDataSource" DataValueField="IdGenero" DataTextField="GeneroLivro" OnPreRender="genDropDownList_PreRender" OnSelectedIndexChanged="genDropDownList_SelectedIndexChanged1" AutoPostBack="true"></asp:DropDownList>
                    <asp:SqlDataSource ID="genSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:BancoConnectionString %>" SelectCommand="SELECT * FROM [Genero]"></asp:SqlDataSource>
                    <br />
                     <asp:TextBox ID="pesquisarTextBox" CssClass="form-control border-end-0 border" runat="server" AutoPostBack="true" />
                    <asp:Button runat="server" ID="pesquisarButton" OnClick="pesquisarButton_Click" CssClass="btnPesquisar btn btn-outline-secondary text-white border-start-0 border ms-n3" Text="Pesquisar">
                    </asp:Button>
                    <br />
                </div>
             <asp:Label ID="alertaLabel" ForeColor="#FBEF70" runat="server" />
            </div>
        <br />
        <div class="container-all-inside">
            <div class="cards-times">

                <asp:Repeater ID="timesRepeater" runat="server" EnableViewState="true">
                    <ItemTemplate>
                        <div class="times" id="<%# DataBinder.Eval(Container.DataItem, "IdLivro") %>" onclick="RedirecionarParaTime(<%# DataBinder.Eval(Container.DataItem, "IdLivro") %>)">
                            <img class="logo-time" src="<%# DataBinder.Eval(Container.DataItem, "CapaLivro") %>" />
                            <div class="text-repeater">
                                <span><%# DataBinder.Eval(Container.DataItem, "TituloLivro") %></span>
                                <br />
                            <span><%# DataBinder.Eval(Container.DataItem, "AutorLivro") %></span>
                            </div>   
                        </div>
                    </ItemTemplate>
                    <FooterTemplate>
                     <asp:Label ID="lblEmptyData"
                            Text="Nenhum livro encontrado" runat="server" Visible="false">
                     </asp:Label> 
                     </table>           
                     </FooterTemplate>
                </asp:Repeater>

            </div>
        </div>
    </div>

    <script src="Assets/js/busca.js"></script>
</asp:Content>
