<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpageside.Master" AutoEventWireup="true" CodeBehind="Relatorio.aspx.cs" Inherits="BiblioTCC.Relatorio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Assets/relatorio.css" />

  

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <div class="relatorio-container">
                <div class="relatorio-container-div">
                    <div class="relatorio-forms-div">
                        <div class="title-div">
                            <asp:Label Text="Relatório" CssClass="title" runat="server" /> &nbsp
                         <i class="fa-sharp fa-regular fa-pen-to-square fa-xl" style="color: #ffd369;"></i>
                        </div>
                        <div class="forms-container">
                            <div class="forms-container-side">
                                <div>
                                    <asp:Label Text="Data Inicial" runat="server" />
                                    <asp:TextBox ID="dataInicialTextBox" CssClass="form-control datepicker" runat="server" placeholder="mm/dd/yyyy" TextMode="Date" ReadOnly="false" />
                                </div>
                                <br />
                                <div>
                                    <asp:Label Text="Data Final" runat="server" />
                                    <asp:TextBox ID="dataFinalTextBox" CssClass="form-control datepicker" runat="server" placeholder="mm/dd/yyyy" TextMode="Date" ReadOnly="false" />
                                </div>
                                <br />
                            </div>
                        </div>
                        <div class="forms-container">
                            <div class="forms-container-side">
                            <div class="forms-container-side">
                                <div>
                                    <asp:Label Text="Tipo do Relatório" runat="server" />
                                    <asp:DropDownList ID="tipoDoRelatorioDropDownList" runat="server" CssClass="form-control" OnPreRender="tipoDoRelatorioDropDownList_PreRender">
                                        <asp:ListItem Value="1">Empréstimo Finalizado</asp:ListItem>
                                        <asp:ListItem Value="2">Empréstimo Ativo</asp:ListItem>
                                        <asp:ListItem Value="3">Empréstimo Pendente</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <br />
                                <br />
                            </div>
                            <div class="forms-container-button-div">
                                <div class="forms-container-button-div-div">
                                    <asp:Button ID="pesquisarButton" CssClass="pesquisarButton" Text="Pesquisar" runat="server" OnClick="pesquisarButton_Click" />
                                    <asp:Button ID="extrairButton" Visible="false" CssClass="extrairButton" Text="Extrair" runat="server" OnClick="extrairButton_Click" />
                                </div>
                                <asp:Label ID="alertaLabel" ForeColor="#FBEF70" runat="server" />
                            </div>
                        </div>
                        <br />
                    </div>
                    <div class="relatorio-gridview-div">
                        <div id="pesquisaDiv" class="forms-grid-div" runat="server" visible="false">
                            <asp:GridView ID="pesquisaGridView" runat="server" Width="100%" CssClass="table table-bordered table-hover table-striped table-condensed custom-gridview" EmptyDataText="Não foi encontrado nenhum resultado com o filtro selecionado." DataSourceID="pesquisaSqlDataSource" OnRowDataBound="pesquisaGridView_RowDataBound" AutoGenerateColumns="true" />
                            <asp:SqlDataSource ID="pesquisaSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:BancoConnectionString %>" />
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:Label ID="lawsonLabel" runat="server" Visible="False" />
    <script src="Assets/js/relatorio.js"></script>

</asp:Content>
