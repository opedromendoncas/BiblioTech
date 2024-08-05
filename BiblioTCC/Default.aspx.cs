using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BiblioTCC
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void AbrirModal(string titulo, string mensagem)
        {
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "Javascript", "javascript: AbrirModal(`" + titulo + "`,`" + mensagem + "`);", true);
        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            if (loginTextBox.Text == "admin" && senhaTextBox.Text == "123@123")
            {
                Response.Redirect("inicio.aspx");
            }
            else
            {
                AbrirModal("Erro", "Senha ou usuário incorreto");
            }

        }
    }
}