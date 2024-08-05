<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpageside.Master" AutoEventWireup="true" CodeBehind="Materiais.aspx.cs" Inherits="BiblioTCC.Materiais" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="Assets/materiais.css" />

   <div class="main">
        <div class="main-content">
            
                <asp:Repeater ID="materiaisRepeater" runat="server" EnableViewState="true">
                    <ItemTemplate>
                        <div class="materiais">
                            <div class="imagem">
                                <img class="logo-img" src="<%# DataBinder.Eval(Container.DataItem, "imagemMaterial") %>" />
                            </div>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" Enabled="false" Text='<%# DataBinder.Eval(Container.DataItem, "tipoMaterial") %>'></asp:TextBox> <br />
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" Enabled="false" Text='<%# DataBinder.Eval(Container.DataItem, "unidadesMaterial") %>'></asp:TextBox> <br />
                            <asp:Button ID="editButton" runat="server" Text="Editar" CssClass="pesquisarButton" CommandName="Edit" OnClick="EditButton_Click" Width="100%" />
                            <asp:Button ID="salvarButton" runat="server" Text="Salvar" CssClass="pesquisarButton" CommandName="Salvar" OnClick="salvarButton_Click" Width="100%" Visible="false" CausesValidation="false" />
                             <asp:HiddenField ID="hiddenIdMaterial" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "IdMaterial") %>' />
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

        </div>
    </div>

    <script src="Assets/js/materiais.js"></script>
</asp:Content>
