using System;
using System.Security.Cryptography;
using System.Text;
using TaskGX.Dados;
using TaskGX.Model;

namespace TaskGX.Servicos
{
    public class ServicoAutenticacao
    {
        // Futuramente pode virar interface (IUsuarioRepository)
        private readonly RepositorioUsuario _usuarioRepository;

        public ServicoAutenticacao()
        {
            _usuarioRepository = new RepositorioUsuario();
        }

        /// <summary>
        /// Valida as credenciais do utilizador.
        /// </summary>
        public bool Login(string nome, string senhaTexto)
        {
            // 1️⃣ Buscar utilizador pelo email
            Usuarios usuario = _usuarioRepository.ObterPorNome(nome);

            if (usuario == null)
                return false;

            // 2️⃣ Gerar hash da senha digitada
            string senhaHash = GerarSha256(senhaTexto);

            // 3️⃣ Comparar com a base de dados
            if (usuario.Senha != senhaHash)
                return false;

            // 4️⃣ Verificar se está ativo
            if (!usuario.Ativo)
                return false;

            return true;
        }

        // 🔐 Hash SHA-256
        private string GerarSha256(string texto)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(texto);
                byte[] hash = sha.ComputeHash(bytes);

                return BitConverter
                    .ToString(hash)
                    .Replace("-", "")
                    .ToLowerInvariant();
            }
        }
    }
}
