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
    public partial class Livro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            codLivroLabel.Text = Request["livro"].ToString();
            if (!IsPostBack)
            {
                Iniciar();
            }
        }

        protected void Iniciar()
        {
            string sql = "SELECT IdLivro, TituloLivro, AutorLivro, GeneroLivro, CapaLivro, SinopseLivro FROM [dbo].[Livros] WHERE IdLivro ="+ codLivroLabel.Text;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);
            SqlCommand comando = new SqlCommand(sql, conn);

            conn.Open();

            SqlDataReader resultado = comando.ExecuteReader();
            if (resultado.Read())
            {
                tituloLivro.InnerText = Convert.ToString(resultado["TituloLivro"]);
                sinopseLivroo.InnerText = Convert.ToString(resultado["SinopseLivro"]);
                autorLivroo.InnerText = Convert.ToString(resultado["AutorLivro"]);
                capaLivro.Src = Convert.ToString(resultado["CapaLivro"]);

            }

            conn.Close();
        }

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                string filename = Path.GetFileName(fileUpload.PostedFile.FileName);
                string ext = Path.GetExtension(fileUpload.FileName);
                fileUpload.SaveAs(Server.MapPath("CapaLivros/" + filename));

                string sql = "UPDATE Livros SET CapaLivro = @CapaLivro WHERE IdLivro ="+ codLivroLabel.Text;
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);
                SqlCommand comando = new SqlCommand(sql, conn);


                comando.Parameters.AddWithValue("@CapaLivro", "CapaLivros/" + filename);


                conn.Open();
                comando.ExecuteReader();
                conn.Close();
            }
            fileUpload.Visible = false;
            btnUpload.Visible = true;
            btnSalvar.Visible = false;
            Iniciar();
        }

        protected void btnVoltar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Busca.aspx");
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            fileUpload.Visible = true;
            btnUpload.Visible = false;
            btnSalvar.Visible = true;
        }
    }


  }
