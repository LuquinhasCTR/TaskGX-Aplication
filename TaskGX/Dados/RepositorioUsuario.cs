using MySql.Data.MySqlClient;
using TaskGX.Model;

namespace TaskGX.Dados
{
    public class RepositorioUsuario
    {
        public Usuarios ObterPorEmail(string email)
        {
            using (var conexao = new MySqlConnection(LigacaoDB.GetConnectionString()))
            {
                conexao.Open();

                string sql = @"
                    SELECT 
                        id, 
                        email, 
                        senha_hash, 
                        ativo
                    FROM usuarios
                    WHERE email = @email
                    LIMIT 1";

                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@email", email);

                    using (var leitor = comando.ExecuteReader())
                    {
                        if (!leitor.Read())
                            return null;

                        return new Usuarios
                        {
                            ID = leitor.GetInt32("id"),
                            Email = leitor.GetString("email"),
                            SenhaHash = leitor.GetString("senha_hash"),
                            Ativo = leitor.GetInt32("ativo") == 1
                        };
                    }
                }
            }
        }
    }
}
