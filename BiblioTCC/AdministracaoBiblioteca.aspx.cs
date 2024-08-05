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
    public partial class AdminitracaoBiblioteca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregartTextBox();
                btnSalvar.Visible = false;
            }
        }


        protected void AbrirModal(string titulo, string mensagem)
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "Javascript", "javascript: AbrirModal(`" + titulo + "`,`" + mensagem + "`);", true);
        }



        protected void CarregartTextBox()
        {
            string sql = "SELECT NomeBiblio, EnderecoBiblio, MultaMaximoDias, TelefoneBiblio, ValorMulta FROM [dbo].[Biblioteca]";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);
            SqlCommand comando = new SqlCommand(sql, conn);

            conn.Open();

            SqlDataReader resultado = comando.ExecuteReader();
            if (resultado.Read())
            {
                bibliotecaTextBox.Text = Convert.ToString(resultado["NomeBiblio"]);
                enderecoTextBox.Text = Convert.ToString(resultado["EnderecoBiblio"]);
                classeDropDownList.SelectedValue = Convert.ToString(resultado["MultaMaximoDias"]);
                telefoneTextBox.Text = Convert.ToString(resultado["TelefoneBiblio"]);
                
            }

            conn.Close();
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {

            btnSalvar.Visible = true;
            btnEditar.Visible = false;
            bibliotecaTextBox.Enabled = true;
            enderecoTextBox.Enabled = true;
            telefoneTextBox.Enabled = true;
          
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
           
               
                string sql = "UPDATE [dbo].[Biblioteca] SET NomeBiblio = @NomeBiblio, EnderecoBiblio = @EnderecoBiblio, TelefoneBiblio = @TelefoneBiblio WHERE IdBiblioteca = @IdBiblioteca ";
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);
                SqlCommand comando = new SqlCommand(sql, conn);

                comando.Parameters.AddWithValue("@NomeBiblio", bibliotecaTextBox.Text);
                comando.Parameters.AddWithValue("@IdBiblioteca", 1);
                comando.Parameters.AddWithValue("@EnderecoBiblio", enderecoTextBox.Text);
                comando.Parameters.AddWithValue("@TelefoneBiblio", telefoneTextBox.Text);

                conn.Open();
                comando.ExecuteReader();
                conn.Close();

                AbrirModal("Sucesso", "ALteração feita com exito");

                CarregartTextBox();
                btnSalvar.Visible = false;
                btnEditar.Visible = true;

                bibliotecaTextBox.Enabled = false;
                enderecoTextBox.Enabled = false;
                telefoneTextBox.Enabled = false;
              
        }
    }
}