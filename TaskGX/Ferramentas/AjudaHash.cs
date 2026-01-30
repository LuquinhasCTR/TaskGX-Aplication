using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace TaskGX.Ferramentas
{
    public static class AjudaHash
    {
        public static string GerarHashSenha(string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
                throw new ArgumentException("Senha inválida.");

            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public static bool VerificarSenha(string senhaDigitada, string hashArmazenado)
        {
            if (string.IsNullOrWhiteSpace(senhaDigitada) || string.IsNullOrEmpty(hashArmazenado))
                return false;

            // Suporte a hashes BCrypt e senha em texto puro (legado)
            if (hashArmazenado.StartsWith("$2"))
            {
                try
                {
                    return BCrypt.Net.BCrypt.Verify(senhaDigitada, hashArmazenado);
                }
                catch
                {
                    return false;
                }
            }

            return senhaDigitada == hashArmazenado;
        }
    }
}
