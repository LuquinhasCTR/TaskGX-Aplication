using System;
using MySql.Data.MySqlClient;
using TaskGX.Model;

namespace TaskGX.Dados
{
    public class RepositorioUsuario
    {
        private string GetConn() => LigacaoDB.GetConnectionString();

        // Recupera usuário por nome
        public Usuarios ObterPorNome(string nome)
        {
            using (var conexao = new MySqlConnection(GetConn()))
            {
                conexao.Open();

                string sql = @"
                    SELECT 
                        ID,
                        Nome,
                        Email,
                        Senha,
                        Avatar,
                        Ativo,
                        EmailVerificado,
                        CodigoVerificacao,
                        CodigoVerificacaoExpiracao,
                        Criado_em,
                        DataAtualizacao
                    FROM Usuarios
                    WHERE Nome = @Nome
                    LIMIT 1;";

                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@Nome", nome);

                    using (var leitor = comando.ExecuteReader())
                    {
                        if (!leitor.Read())
                            return null;

                        var usuario = new Usuarios
                        {
                            ID = leitor["ID"] != DBNull.Value ? Convert.ToInt32(leitor["ID"]) : 0,
                            Nome = leitor["Nome"] as string,
                            Email = leitor["Email"] as string,
                            Senha = leitor["Senha"] as string,
                            Avatar = leitor["Avatar"] != DBNull.Value ? leitor["Avatar"].ToString() : null,
                            Ativo = leitor["Ativo"] != DBNull.Value ? Convert.ToBoolean(leitor["Ativo"]) : true,
                            EmailVerificado = leitor["EmailVerificado"] != DBNull.Value ? Convert.ToBoolean(leitor["EmailVerificado"]) : false,
                            CodigoVerificacao = leitor["CodigoVerificacao"] != DBNull.Value ? leitor["CodigoVerificacao"].ToString() : null,
                            CodigoVerificacaoExpiracao = leitor["CodigoVerificacaoExpiracao"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(leitor["CodigoVerificacaoExpiracao"]) : null,
                            CriadoEm = leitor["Criado_em"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(leitor["Criado_em"]) : null,
                            DataAtualizacao = leitor["DataAtualizacao"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(leitor["DataAtualizacao"]) : null
                        };

                        return usuario;
                    }
                }
            }
        }

        // Verifica se email já existe
        public bool ExisteEmail(string email)
        {
            using (var conexao = new MySqlConnection(GetConn()))
            {
                conexao.Open();

                string sql = "SELECT COUNT(1) FROM Usuarios WHERE Email = @Email;";

                using (var cmd = new MySqlCommand(sql, conexao))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    var result = cmd.ExecuteScalar();
                    return Convert.ToInt64(result) > 0;
                }
            }
        }

        // Insere novo usuário
        public void Inserir(Usuarios usuario)
        {
            using (var conexao = new MySqlConnection(GetConn()))
            {
                conexao.Open();

                string sql = @"
                    INSERT INTO Usuarios
                    (Nome, Email, Senha, Avatar, Ativo, EmailVerificado, CodigoVerificacao, CodigoVerificacaoExpiracao, Criado_em, DataAtualizacao)
                    VALUES
                    (@Nome, @Email, @Senha, @Avatar, @Ativo, @EmailVerificado, @CodigoVerificacao, @CodigoVerificacaoExpiracao, @Criado_em, @DataAtualizacao);";

                using (var comando = new MySqlCommand(sql, conexao))
                {
                    comando.Parameters.AddWithValue("@Nome", usuario.Nome);
                    comando.Parameters.AddWithValue("@Email", usuario.Email);
                    comando.Parameters.AddWithValue("@Senha", usuario.Senha);
                    comando.Parameters.AddWithValue("@Avatar", (object)usuario.Avatar ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Ativo", usuario.Ativo);
                    comando.Parameters.AddWithValue("@EmailVerificado", usuario.EmailVerificado);
                    comando.Parameters.AddWithValue("@CodigoVerificacao", (object)usuario.CodigoVerificacao ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@CodigoVerificacaoExpiracao", (object)usuario.CodigoVerificacaoExpiracao ?? DBNull.Value);
                    comando.Parameters.AddWithValue("@Criado_em", (object)usuario.CriadoEm ?? DateTime.Now);
                    comando.Parameters.AddWithValue("@DataAtualizacao", (object)usuario.DataAtualizacao ?? DateTime.Now);

                    comando.ExecuteNonQuery();

                    // Atribui o ID gerado pelo MySQL ao objeto (LastInsertedId é long)
                    try
                    {
                        var last = comando.LastInsertedId;
                        if (last > 0)
                            usuario.ID = Convert.ToInt32(last);
                    }
                    catch
                    {
                        // Se por algum motivo LastInsertedId não estiver disponível,
                        // não é crítico — o usuário já foi inserido.
                    }
                }
            }
        }
    }
}