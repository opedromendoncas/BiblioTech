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


namespace BiblioTCC
{
    public partial class CadastroLivros : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Iniciar();
            }
        }

        protected void Iniciar()
        {
            PreencherGenero();
        }
        protected void AbrirModal(string titulo, string mensagem)
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "Javascript", "javascript: AbrirModal(`" + titulo + "`,`" + mensagem + "`);", true);
        }

        protected void AbrirModal2()
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "Javascript", "javascript: AbrirModal2();", true);
        }

        protected void PreencherGenero()
        {
            string sql = "SELECT IdGenero, GeneroLivro FROM [dbo].[Genero]";
            genSqlDataSource.SelectCommand = sql;

        }

        protected void genDropDownList_PreRender(object sender, EventArgs e)
        {
            genDropDownList.Items.Remove("");
            genDropDownList.Items.Insert(0, "");
        }



        protected void CadastrarNoBanco(string AutorLivro)
        {
            string filename = Path.GetFileName(uploadCapa.PostedFile.FileName);
            string ext = Path.GetExtension(uploadCapa.FileName);
            uploadCapa.SaveAs(Server.MapPath("CapaLivros/" + filename));
            string sql = "INSERT INTO [dbo].[Livros] (AutorLivro, GeneroLivro, TituloLivro, CapaLivro, Tombo, SinopseLivro) VALUES (@AutorLivro, @GeneroLivro, @TituloLivro, @CapaLivro, @Tombo, @SinopseLivro)";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);
            SqlCommand comando = new SqlCommand(sql, conn);

            comando.Parameters.AddWithValue("@AutorLivro", autorTextBox.Text);
            comando.Parameters.AddWithValue("@TituloLivro", tituloTextBox.Text);
            comando.Parameters.AddWithValue("@GeneroLivro", genDropDownList.SelectedValue);
            comando.Parameters.AddWithValue("@CapaLivro", "CapaLivros/" + filename);
            comando.Parameters.AddWithValue("@Tombo", tomboTextBox.Text);
            comando.Parameters.AddWithValue("@SinopseLivro", sinopseTextBoxx.Text);

            conn.Open();
            comando.ExecuteReader();
            conn.Close();

            LimparCampos();
        }

        protected void btnEntrar_Click(object sender, EventArgs e)

        {
            ValidarPreenchimentoDosCampos();
            if (uploadCapa.HasFile != false)
            {
                int megabyteMaximo = 10 * 1000000;
                int tamanhoArquivo = uploadCapa.PostedFile.ContentLength;

                if (tamanhoArquivo <= megabyteMaximo)
                {
                    CadastrarNoBanco("1");


                    AbrirModal("Sucesso!", "Livro cadastrado com exito!");

                }

            }
            else
            {
                AbrirModal("Atenção", "necessário inserir a capa do livro");
            }

        }


        protected void ValidarPreenchimentoDosCampos()
        {

            if (string.IsNullOrEmpty(tituloTextBox.Text) && string.IsNullOrEmpty(autorTextBox.Text))
            {
                AbrirModal("Atenção", "Campo titulo e autor esta em branco.");
            }

            if (string.IsNullOrEmpty(autorTextBox.Text))
            {
                AbrirModal("Atençãa", "Campo autor esta em branco.");
            }


            if (string.IsNullOrEmpty(tituloTextBox.Text))
            {
                AbrirModal("Atenção", "Campo titulo esta em branco.");
            }

        }

        protected void LimparCampos()
        {
            tituloTextBox.Text = "";
            autorTextBox.Text = "";
            tomboTextBox.Text = "";
            sinopseTextBoxx.Text = "";
            PreencherGenero();

        }

        protected void btnMassa_Click(object sender, EventArgs e)
        {
            AbrirModal2();
        }

        protected void csvLinkButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ArquivoUpload/upload_livro.csv");
        }


 

        private void ImportarMopOnline(StreamReader arquivo)
        {
            teste imp = new teste();
            imp.importarArquivo(arquivo);

        }

        protected void btnCSV_Click(object sender, EventArgs e)
        {
            if (csvFileUpload.HasFile != false)
            {
                StreamReader arquivo = new StreamReader(csvFileUpload.PostedFile.InputStream, Encoding.Default);
                ImportarMopOnline(arquivo);
                csvFileUpload.Enabled = false;

            }
        }
    }
}