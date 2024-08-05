using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Data;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;


namespace BiblioTCC
{
    public partial class CadastroEmprestimo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Iniciar();
            }
        }
        #region Iniciar Abas
        protected void Iniciar()
        {
            AcessarAba(1);
         

        }
        protected void TrocarAbas(object sender, EventArgs e)
        {
            int NumeroDaAba = Convert.ToInt32(((LinkButton)sender).CommandArgument);

            AcessarAba(NumeroDaAba);
        }

        protected void AcessarAba(int NumeroDaAba)
        {
            switch (NumeroDaAba)
            {
                case 1:
                    {
                        novoEmprestimoDiv.Visible = true;
                        validarEmprestimo.Visible = false;

                        tab1.CssClass = "Clicked";
                        tab2.CssClass = "Initial";

                        emprestimoLi.Attributes.Add("class", "active");
                        validarLi.Attributes.Add("class", "");

                        dataEmprestimoTextBox.Visible = true;
                        nomeTextBox.Visible = true;
                        emailTextBox.Visible = true;
                        livroTextBox.Visible = true;
                        tomboTextBox.Visible = true;


                        break;
                    }
                case 2:
                    {
                        novoEmprestimoDiv.Visible = false;
                        validarEmprestimo.Visible = true;

                        tab1.CssClass = "Initial";
                        tab2.CssClass = "Clicked";

                        validarLi.Attributes.Add("class", "active");
                        emprestimoLi.Attributes.Add("class", "");

                        dataEmprestimoTextBox.Visible = false;
                        nomeTextBox.Visible = false;
                        emailTextBox.Visible = false;
                        livroTextBox.Visible = false;
                        tomboTextBox.Visible = false;

                        PreencherEmprestimoGridView();

                        break;
                    }
            }
        }
        #endregion
        #region Cadastrar Empréstimo No Banco

        public void RegistrarEmprestimo(string nomeUsuario)
        {
            //convertendo o valor da textbox para date
            DateTime dataSelecionada = DateTime.Parse(dataEmprestimoTextBox.Text);

            // definir a conexão com o banco de dados
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);
           

            // abrir a conexão com o banco de dados
            conn.Open();

            // inserir os dados do usuário na tabela Usuario
            string queryUsuario = "INSERT INTO Usuario (NomeUsuario, EmailUsuario) VALUES (@NomeUsuario, @EmailUsuario); SELECT SCOPE_IDENTITY();";
            SqlCommand commandUsuario = new SqlCommand(queryUsuario, conn);
            commandUsuario.Parameters.AddWithValue("@NomeUsuario", nomeTextBox.Text);
            commandUsuario.Parameters.AddWithValue("@EmailUsuario", emailTextBox.Text);
            int idUsuario = Convert.ToInt32(commandUsuario.ExecuteScalar());

            // inserir os dados do empréstimo na tabela Emprestimo
            string queryEmprestimo = "INSERT INTO Emprestimo (IdUsuario, DataEmprestimo, Status) VALUES (@IdUsuario, @DataEmprestimo, @Status); SELECT SCOPE_IDENTITY();";
            SqlCommand commandEmprestimo = new SqlCommand(queryEmprestimo, conn);
            commandEmprestimo.Parameters.AddWithValue("@IdUsuario", idUsuario);
            commandEmprestimo.Parameters.Add("@DataEmprestimo", SqlDbType.Date).Value = dataSelecionada;
            commandEmprestimo.Parameters.AddWithValue("@Status", 1);
            int idEmprestimo = Convert.ToInt32(commandEmprestimo.ExecuteScalar());

            // inserir os dados do livro na tabela ItensEmprestimo
            string querySocorro = "INSERT INTO LivrosEmprestimo (IdEmprestimo, IdLivro,Livros) VALUES (@IdEmprestimo, @IdLivro, @Livros)";
            SqlCommand commandSocorro = new SqlCommand(querySocorro, conn);
            commandSocorro.Parameters.AddWithValue("@IdEmprestimo", idEmprestimo);
            commandSocorro.Parameters.AddWithValue("@IdLivro", tomboTextBox.Text);
            commandSocorro.Parameters.AddWithValue("@Livros", livroTextBox.Text);
            commandSocorro.ExecuteNonQuery();

            // fechar a conexão com o banco de dados
            conn.Close();
            LimparCampos();

        }


        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nomeTextBox.Text) && string.IsNullOrEmpty(emailTextBox.Text))
            {
                AbrirModal("Atenção", "Campo titulo e autor esta em branco.");
            }
            else
            {
                RegistrarEmprestimo("1");
                AbrirModal("Sucesso","Cadastro realizado");
            }
         
            
        }

        protected void LimparCampos()
        {
            nomeTextBox.Text = "";
            emailTextBox.Text = "";
            tomboTextBox.Text = "";
            dataEmprestimoTextBox.Text = "";
            livroTextBox.Text = "";


        }
     
     
        #endregion
        #region GridView e suas funções
        protected void PreencherEmprestimoGridView()
        {
            ExecutarProcedureAtualizacaoStatusEmprestimo();
            string sql = "SELECT e.IdEmprestimo, u.NomeUsuario as Nome, u.EmailUsuario As Email, lp.Livros As LivrosEmprestados, FORMAT(e.DataEmprestimo, 'dd/MM/yyyy') as [Data de Empréstimo], s.Status AS Status FROM dbo.Usuario u LEFT JOIN dbo.Emprestimo e ON u.IdUsuario = e.IdUsuario LEFT JOIN dbo.LivrosEmprestimo lp ON lp.IdEmprestimo = e.IdEmprestimo LEFT JOIN dbo.Status s ON s.IdStatus = e.Status WHERE e.Status <> 2";
            usuarioSqlDataSource.SelectCommand = sql;
        }

        protected void validarEmprestimoGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Visible = false;

                // Obtém o valor da data de empréstimo da célula desejada
                string dataEmpréstimo = e.Row.Cells[4].Text;

                // Converte o valor da data para um objeto DateTime
                DateTime data;
                if (DateTime.TryParse(dataEmpréstimo, out data))
                {
                    // Verifica se a data de empréstimo passou de 7 dias
                    if (DateTime.Now > data.AddDays(7))
                    {
                        // Altera a cor da célula que contém o nome do usuário para vermelho
                        e.Row.Cells[2].ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[3].ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[4].ForeColor = System.Drawing.Color.Red;
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            else if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Visible = false;
                e.Row.CssClass = "custom-header";
            }
        }
        public void ExecutarProcedureAtualizacaoStatusEmprestimo()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString))
            {
                SqlCommand command = new SqlCommand("AtualizarStatusEmprestimo", conn);
                command.CommandType = CommandType.StoredProcedure;
                conn.Open();
                command.ExecuteNonQuery();

                // Verificar os empréstimos com mais de 7 dias e enviar e-mail para os usuários
                string query = "SELECT Usuario.EmailUsuario, LivrosEmprestimo.Livros FROM Emprestimo " +
                               "INNER JOIN Usuario ON Emprestimo.IdUsuario = Usuario.IdUsuario " +
                               "INNER JOIN LivrosEmprestimo ON Emprestimo.IdEmprestimo = LivrosEmprestimo.IdEmprestimo " +
                               "WHERE Emprestimo.DataEmprestimo <= DATEADD(day, -7, GETDATE()) AND Emprestimo.Status = 1";
                SqlCommand commandVerificarEmprestimos = new SqlCommand(query, conn);
                using (SqlDataReader reader = commandVerificarEmprestimos.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string emailUsuario = reader["EmailUsuario"].ToString();
                        string livroEmprestado = reader["Livros"].ToString();

                        // Enviar e-mail de notificação para o usuário
                        string assunto = "Aviso de empréstimo atrasado ETEC Irmã Agostina";
                        string corpoMensagem = $"Seu empréstimo do livro(s) '{livroEmprestado}' está atrasado. Favor devolvê-lo o mais rápido possível.";
                        EnviarEmail(emailUsuario, assunto, corpoMensagem);
                    }
                }
            }
        }

        public void EnviarEmail(string emailDestinatario, string assunto, string corpoMensagem)
        {
            // Configurações do cliente SMTP do Gmail
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("biblio.tech.etec@gmail.com", "Biblioteca2023@");

            // Criação da mensagem de e-mail
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("seu-email@gmail.com");
            mailMessage.To.Add(emailDestinatario);
            mailMessage.Subject = assunto;
            mailMessage.Body = corpoMensagem;

            // Envio do e-mail
            smtpClient.Send(mailMessage);
        }
        protected void validarEmprestimoGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            validarEmprestimoGridView.PageIndex = e.NewPageIndex;
            PreencherEmprestimoGridView();
            validarEmprestimoGridView.DataBind();
        }

     

        protected GridViewRow InstanciarLinha(object sender)
        {
            LinkButton lnk = (LinkButton)sender;
            TableCell cell = new TableCell();
            cell = (TableCell)lnk.Parent;
            GridViewRow row = (GridViewRow)cell.Parent;
            return row;
        }

        protected void finalizarStatusLinkButton_Click(object sender, EventArgs e)
        {
            int linha = InstanciarLinha(sender).RowIndex;
            string cod_teste = validarEmprestimoGridView.Rows[linha].Cells[1].Text;

            // Armazena o IdEmprestimo em uma variável de sessão para ser acessada no evento do botão de confirmação
            Session["IdEmprestimo"] = cod_teste;

            // Exibe o modal de confirmação
            ScriptManager.RegisterStartupScript(this, GetType(), "exibirModal", "exibirModalConfirmacao();", true);
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            // Obtém o IdEmprestimo armazenado na variável de sessão
            string cod_teste = Session["IdEmprestimo"].ToString();

            // Realiza o update no banco de dados
            string sql = "UPDATE [dbo].[Emprestimo] SET Status = @Status, DataFinal = @DataFinal WHERE IdEmprestimo = @IdEmprestimo";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);
            SqlCommand comando = new SqlCommand(sql, conn);

            comando.Parameters.AddWithValue("@IdEmprestimo", cod_teste);
            comando.Parameters.AddWithValue("@Status", 2);
            comando.Parameters.AddWithValue("@DataFinal", DateTime.Now.Date);

            conn.Open();
            comando.ExecuteNonQuery();
            conn.Close();

            // Exibe uma mensagem de sucesso
            AbrirModal("Sucesso", "Empréstimo Finalizado");

            PreencherEmprestimoGridView();
            validarEmprestimoGridView.DataBind();

            // Fecha o modal de confirmação
            ScriptManager.RegisterStartupScript(this, GetType(), "fecharModal", "fecharModalConfirmacao();", true);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            // Limpa a variável de sessão
            Session.Remove("IdEmprestimo");
        }





        #endregion
        #region Modal e Botões
        protected void AbrirModal(string titulo, string mensagem)
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "Javascript", "javascript: AbrirModal(`" + titulo + "`,`" + mensagem + "`);", true);
        }

        protected void AbrirModal2()
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "Javascript", "javascript: AbrirModal2();", true);
        }
      

        protected void csvLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ArquivoUpload/emprestimo.csv");
        }

        #endregion
















        //protected void CarregarUsuarioDoBanco()

        //{

        //    string sql = "SELECT IdUsuario FROM [dbo].[Usuario]";

        //    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);

        //    SqlCommand comando = new SqlCommand(sql, conn);




        //    conn.Open();




        //    SqlDataReader resultado = comando.ExecuteReader();

        //    if (resultado.Read())

        //    {

        //        codIdUsuario.Text = Convert.ToString(resultado["IdUsuario"]);

        //    }

        //    conn.Close();

        //}





        //protected void SubirEmprestimoNoBanco(string dataEmprestimo)
        //{
        //    CarregarUsuarioDoBanco();
        //    string sql = "INSERT INTO [dbo].[Emprestimo] (DataEmprestimo, IdUsuario) VALUES (@DataEmprestimo, @IdUsuario)";
        //    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);
        //    SqlCommand comando = new SqlCommand(sql, conn);

        //    comando.Parameters.AddWithValue("@DataEmprestimo", dataEmprestimoTextBox.Text);
        //    comando.Parameters.AddWithValue("@IdUsuario", codIdUsuario.Text);


        //    conn.Open();
        //    comando.ExecuteReader();
        //    conn.Close();

        //    LimparCampos();
        //}
        //}

        //public void SubirEmprestimoNoBanco(string nomeUsuario)
        //{
        //    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString))
        //    {
        //        string sql = "INSERT INTO dbo.Emprestimo (IdUsuario, DataEmprestimo) " +
        //                     "VALUES ((SELECT IdUsuario FROM dbo.Usuario WHERE NomeUsuario = @NomeUsuario AND EXISTS (SELECT * FROM dbo.Usuario WHERE NomeUsuario = @NomeUsuario)), @DataEmprestimo)";

        //        using (SqlCommand comando = new SqlCommand(sql, conn))
        //        {
        //            comando.Parameters.AddWithValue("@NomeUsuario", nomeUsuario);
        //            comando.Parameters.AddWithValue("@DataEmprestimo", dataEmprestimoTextBox.Text);

        //            conn.Open();
        //            comando.ExecuteNonQuery();
        //            conn.Close();
        //        }
        //    }
        //}



        // public void SubirEmprestimoNoBanco(string codLivro)
        //{
        //     using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString))

        //     {
        //        string sql = "INSERT INTO dbo.Emprestimo (IdUsuario, DataEmprestimo) " +
        //                     "VALUES ((SELECT IdUsuario FROM dbo.Usuario WHERE IdUsuario = @IdUsuario AND EXISTS (SELECT * FROM dbo.Usuario WHERE IdUsuario = @IdUsuario)), @DataEmprestimo)";

        //       using (SqlCommand comando = new SqlCommand(sql, conn))
        //       {
        //           comando.Parameters.AddWithValue("@IdUsuario", codLivroLabel.Text);
        //           comando.Parameters.AddWithValue("@DataEmprestimo", dataEmprestimoTextBox.Text);

        //           conn.Open();
        //            comando.ExecuteNonQuery();
        //            conn.Close();

        //         }
        //    }
        // }

    }
}