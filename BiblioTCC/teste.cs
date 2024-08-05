using System;
using System.Linq;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;

namespace BiblioTCC
{
    public class teste
    {
        public string AutorLivro { get; set; }
        public string GeneroLivro { get; set; }
        public string TituloLivro { get; set; }
        public string Tombo { get; set; }
        public string SinopseLivro { get; set; }
        public int NumLinha { get; set; }
        public int Problema { get; set; }



        public void importarArquivo(StreamReader ler)
        {
            string linha = null;
            string[] linhaSeparada = null;
            NumLinha = 1;

            while ((linha = ler.ReadLine()) != null)
            {
                linhaSeparada = linha.Split(';');
                Problema = 0;

                if (NumLinha > 1 && !string.IsNullOrEmpty(linhaSeparada[0]))
                {
                    if (linha.Split(';').Count() == 5)
                    {
                        AutorLivro = linhaSeparada[0].Trim();
                        GeneroLivro = linhaSeparada[1].Trim();
                        TituloLivro = linhaSeparada[2].TrimEnd();
                        Tombo = linhaSeparada[3];
                        SinopseLivro = linhaSeparada[4];

                        InserirArquivo();

                    }
                }
                NumLinha++;
            }
         
        }

        private void InserirArquivo()
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["BancoConnectionString"].ConnectionString);
            string sql = "INSERT INTO [dbo].[Livros] (AutorLivro, GeneroLivro, TituloLivro, Tombo, SinopseLivro) VALUES (@AutorLivro, @GeneroLivro, @TituloLivro, @Tombo, @SinopseLivro)";
            SqlCommand comando = new SqlCommand(sql, conn);

            comando.Parameters.AddWithValue("@AutorLivro", AutorLivro);
            comando.Parameters.AddWithValue("@GeneroLivro", GeneroLivro);
            comando.Parameters.AddWithValue("@TituloLivro", TituloLivro);
            comando.Parameters.AddWithValue("@Tombo", Tombo);
            comando.Parameters.AddWithValue("@SinopseLivro", SinopseLivro);
            

            conn.Open();
            comando.ExecuteReader();
            conn.Close();
            conn.Dispose();
        }
    }

  
      

    

}