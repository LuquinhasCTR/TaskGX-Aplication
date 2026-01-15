using MySql.Data.MySqlClient;
using System.Configuration;
using TaskGX.Model;

namespace TaskGX.Dados
{
    public class RepositorioUsuario
    {
        public Usuarios ObterPorNome(string nome)
        {
            using (var conexao = new MySqlConnection(
                ConfigurationManager.ConnectionStrings["MySqlConnection"].ConnectionString))
            {
                conexao.Open();

                string sql = @"
                    SELECT 
                        id,
                        nome,
                        email,
                        senha,
                        ativo
                    FROM usuarios
                    WHERE nome = @Nome
                    LIMIT 1";

                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@Nome", nome);

                    using (var leitor = comando.ExecuteReader())
                    {
                        if (!leitor.Read())
                            return null;

                        return new Usuarios
                        {
                            ID = leitor.GetInt32("id"),
                            Nome = leitor.GetString("nome"),
                            Email = leitor.GetString("email"),
                            Senha = leitor.GetString("senha"),
                            Ativo = leitor.GetInt32("ativo") == 1
                        };
                    }
                }
            }
        }
    }
}
