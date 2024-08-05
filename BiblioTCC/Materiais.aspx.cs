using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Data;
using System.Data.Sql;
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
    public partial class Materiais : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PreencherListaDeMateriais();
            }
        }
        #region Dados AspRepeater
        protected void PreencherListaDeMateriais()
        {
            string sql = "SELECT IdMaterial, tipoMaterial, unidadesMaterial, imagemMaterial FROM [dbo].[Material]";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            materiaisRepeater.DataSource = dt;
            materiaisRepeater.DataBind();
        }
        #endregion

        #region Botões de controle 
        protected void EditButton_Click(object sender, EventArgs e)
        {
            Button editButton = (Button)sender;
            RepeaterItem item = (RepeaterItem)editButton.NamingContainer;
            TextBox textBox1 = (TextBox)item.FindControl("TextBox1");
            TextBox textBox2 = (TextBox)item.FindControl("TextBox2");
            Button salvarButton = (Button)item.FindControl("salvarButton");

            // Alternar o estado de edição das TextBox
            textBox1.Enabled = !textBox1.Enabled;
            textBox2.Enabled = !textBox2.Enabled;

            if (textBox1.Enabled && textBox2.Enabled)
            {
                // Modo de edição ativado, tornar o botão "Editar" invisível e o botão "Salvar" visível
                editButton.Visible = false;
                salvarButton.Visible = true;

                // Obter o valor do IdMaterial do RepeaterItem
                string idMaterial = ((HiddenField)item.FindControl("hiddenIdMaterial")).Value;
                salvarButton.CommandArgument = idMaterial; // Armazenar o IdMaterial no CommandArgument do botão "Salvar"
            }
        }

        protected void salvarButton_Click(object sender, EventArgs e)
        {
            Button salvarButton = (Button)sender;
            RepeaterItem item = (RepeaterItem)salvarButton.NamingContainer;
            TextBox textBox1 = (TextBox)item.FindControl("TextBox1");
            TextBox textBox2 = (TextBox)item.FindControl("TextBox2");

            // Modo de edição desativado, tornar o botão "Salvar" invisível e o botão "Editar" visível
            salvarButton.Visible = false;
            Button editButton = (Button)item.FindControl("editButton");
            editButton.Visible = true;

            // Obtem o valor do IdMaterial do CommandArgument
            string idMaterial = salvarButton.CommandArgument;

            // Atualizar no banco de dados
            string tipoMaterial = textBox1.Text;
            string unidadesMaterial = textBox2.Text;

            AtualizarMaterial(idMaterial, tipoMaterial, unidadesMaterial);

            // Atualizar a exibição do Repeater
            PreencherListaDeMateriais();
        }
        #endregion

        #region Atualização no Banco
        private void AtualizarMaterial(string idMaterial, string tipoMaterial, string unidadesMaterial)
        {
            // Lógica para atualizar o material no banco de dados
            string sql = "UPDATE [dbo].[Material] SET tipoMaterial = @TipoMaterial, unidadesMaterial = @UnidadesMaterial WHERE IdMaterial = @IdMaterial";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@TipoMaterial", tipoMaterial);
            cmd.Parameters.AddWithValue("@UnidadesMaterial", unidadesMaterial);
            cmd.Parameters.AddWithValue("@IdMaterial", idMaterial);


            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        #endregion
    }
}