using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace BiblioTCC
{
    public partial class Relatorio : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Iniciar()
        {

        }



        protected void tipoDoRelatorioDropDownList_PreRender(object sender, EventArgs e)
        {
            tipoDoRelatorioDropDownList.Items.Remove("");
            tipoDoRelatorioDropDownList.Items.Insert(0, "");
        }

        protected void pesquisarButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(dataInicialTextBox.Text))
            {
                if (!string.IsNullOrEmpty(dataFinalTextBox.Text))
                {
                    if (!string.IsNullOrEmpty(tipoDoRelatorioDropDownList.SelectedValue))
                    {

                        CarregarRelatorioGridView();
                        pesquisaDiv.Visible = true;

                    }
                    else
                    {
                        alertaLabel.Text = @"É necessário preencher o campo ""Tipo do Relatório"" antes de prosseguir.";
                    }
                }
                else
                {
                    alertaLabel.Text = @"É necessário preencher o campo ""Data Final"" antes de prosseguir.";
                }
            }
            else
            {
                alertaLabel.Text = @"É necessário preencher o campo ""Data Inicial"" antes de prosseguir.";
            }
        }

        private string FormatarData(string data)
        {
            string dataTexto = "";

            if (data.Length >= 10)
            {
                dataTexto = data.Substring(6, 4) + "-" + data.Substring(3, 2) + "-" + data.Substring(0, 2);
            }

            return dataTexto;
        }

        private void CarregarRelatorioGridView()
        {
            alertaLabel.Text = "";

            string dataInicial = dataInicialTextBox.Text;
            string dataFinal = dataFinalTextBox.Text;

            string sql = "";


            switch (tipoDoRelatorioDropDownList.SelectedValue)
            {
                case "1":
                    sql = $"SELECT u.IdUsuario AS ID, u.NomeUsuario AS Nome, u.EmailUsuario AS Email, FORMAT(e.DataEmprestimo, 'dd/MM/yyyy') AS [Data de Empréstimo], FORMAT(e.DataFinal, 'dd/MM/yyyy') AS [Data de Entrega], s.Status AS Status FROM dbo.Usuario u LEFT JOIN dbo.Emprestimo e ON u.IdUsuario = e.IdUsuario LEFT JOIN dbo.Status s ON s.IdStatus = e.Status WHERE e.Status IN (2) AND e.DataEmprestimo >= '{dataInicial}' AND e.DataEmprestimo <= '{dataFinal}' ORDER BY e.DataEmprestimo;";
                    break;
                case "2":
                    sql = $"SELECT u.IdUsuario As ID, u.NomeUsuario as Nome, u.EmailUsuario As Email, FORMAT(e.DataEmprestimo, 'dd/MM/yyyy') as [Data de Empréstimo],  FORMAT(e.DataFinal, 'dd/MM/yyyy') as [Data Entrega], s.Status AS Status FROM dbo.Usuario u LEFT JOIN dbo.Emprestimo e ON u.IdUsuario = e.IdUsuario LEFT JOIN dbo.Status s ON s.IdStatus = e.Status WHERE e.Status IN (1) AND e.DataEmprestimo >= '{dataInicial}' AND e.DataEmprestimo <= '{dataFinal}' ORDER BY e.DataEmprestimo;";
                    break;
                case "3":
                    sql = $"SELECT u.IdUsuario As ID, u.NomeUsuario as Nome, u.EmailUsuario As Email, FORMAT(e.DataEmprestimo, 'dd/MM/yyyy') as [Data de Empréstimo], s.Status AS Status FROM dbo.Usuario u LEFT JOIN dbo.Emprestimo e ON u.IdUsuario = e.IdUsuario LEFT JOIN dbo.Status s ON s.IdStatus = e.Status WHERE e.Status IN (3) AND e.DataEmprestimo >= '{dataInicial}' AND e.DataEmprestimo <= '{dataFinal}' ORDER BY e.DataEmprestimo;";
                    break;
            }




            pesquisaSqlDataSource.SelectCommand = sql;
        }

        protected void VerificarSePossuiMaisDeUmaLinha(string sql)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);
            SqlCommand comando = new SqlCommand(sql, conn);
            conn.Open();
            SqlDataReader resultadoSql = comando.ExecuteReader();

            if (resultadoSql.Read())
            {
                if (!string.IsNullOrEmpty(Convert.ToString(resultadoSql["IdUsuario"])))
                {
                    extrairButton.Visible = true;
                }
            }

            conn.Close();
        }

        protected void extrairButton_Click(object sender, EventArgs e)
        {

        }

        protected void exportarExcelLinkButton_Click(object sender, EventArgs e)
        {

        }

        protected void pesquisaGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Visible = false;

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
                e.Row.Cells[0].Visible = false;
                e.Row.CssClass = "custom-header";
            }

        }
    }
}