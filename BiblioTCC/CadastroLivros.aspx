<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroLivros.aspx.cs" Inherits="BiblioTCC.CadastroLivros" %>
<%@ Assembly Src="teste.cs" %>


    <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

     <link rel="stylesheet" href="Assets/cadastroLivros.css" />
    <script src="//code.jquery.com/jquery-1.11.2.min.js" type="text/javascript"></script>
<script type="text/javascript">
    function ShowImagePreview(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $('#<%=ImgPrv.ClientID%>').prop('src', e.target.result)
                    .width(360)
                    .height(400);
            };
            reader.readAsDataURL(input.files[0]);
        }
    }
</script>

    <div class="container-all">
        <div class="left">
            <h2>Novo Livro <img src="Assets/img/add-books.png" alt="Alternate Text" height="30px" /> </h2>
            <label>Selecione o gênero</label>
            <asp:DropDownList ID="genDropDownList" runat="server" CssClass="form-select" DataSourceID="genSqlDataSource"  DataValueField="IdGenero" DataTextField="GeneroLivro" OnPreRender="genDropDownList_PreRender" AutoPostBack="true"></asp:DropDownList>
            <asp:SqlDataSource ID="genSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:BancoConnectionString %>" SelectCommand="SELECT * FROM [Genero]"></asp:SqlDataSource>
            <br />
            <label>Titulo</label>
            <asp:TextBox ID="tituloTextBox" runat="server" CssClass="form-control" placeholder="Digite o título do livro"></asp:TextBox>
            <br />
            <label>Autor</label>
            <asp:TextBox ID="autorTextBox" runat="server" CssClass="form-control" placeholder="Digite o autor do livro"></asp:TextBox>
            <br />
            <label>Tombo</label>
            <asp:TextBox ID="tomboTextBox" runat="server" CssClass="form-control" placeholder="Digite o número do tombo"></asp:TextBox>
            <br />
            <label>Sinopse</label>
            <asp:TextBox ID="sinopseTextBoxx" runat="server" CssClass="form-control"  placeholder="Digite a sinopse do livro" TextMode="MultiLine" Rows="5"></asp:TextBox>
            <br />
            <div class="row">
                <div class="col-md-3">
                    <asp:Button ID="btnEntrar" CssClass="pesquisarButton" runat="server" Text="Cadastrar" OnClick="btnEntrar_Click" />
                </div>
                <div class="col-md-1"></div>
                <div class="col-md-3">
                    <asp:Button ID="btnMassa" runat="server" Text="Importar Livros em Massa" CssClass="pesquisarButton" CausesValidation="False" OnClick="btnMassa_Click"/>
                </div>
            </div>
        </div>
        <div class="right">
            <br />
            <br />
            <label>Selecione a capa</label>
           <div>
            <fieldset style="width: 300px;">
                 <asp:FileUpload ID="uploadCapa" runat="server" onchange="ShowImagePreview(this);" />
                <br /> <br />
                <legend id="legenda">&nbsp Prévia da capa escolhida</legend>
                <div style="text-align: center;">
                    <asp:Image ID="ImgPrv" Height="400px" Width="360px" runat="server" /><br />
                </div>
            </fieldset>
            <br />
            <br />
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


     <div id="modal2">
        <div>
            <h2>Selecione o arquivo .csv</h2>
            <p>
                <asp:LinkButton ID="csvLinkButton" runat="server" CausesValidation="false" OnClick="csvLinkButton_Click">Baixar Modelo Arquivo .CSV</asp:LinkButton>
                <br />
                <asp:FileUpload ID="csvFileUpload" runat="server" />
              
            </p>
        </div>
        <div class="modal-button">
            <a id="modal-button-close2" onclick="FecharModal2()">Fechar</a>&nbsp<asp:Button ID="btnCSV" CssClass="ver-card-button" runat="server" Text="Importar" OnClick="btnCSV_Click"/>
        </div>
    </div>



    <script type="text/javascript" src="Scripts/jquery-3.4.1.min.js "></script> 
    <script src="Assets/js/cadastroLivros.js"></script>
</asp:Content>