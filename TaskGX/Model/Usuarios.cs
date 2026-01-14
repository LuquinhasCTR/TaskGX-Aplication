using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskGX.Model
{
    /// <summary>
    /// Dados do usuário
    /// </summary>
    public class Usuarios
    {
        public int ID { get; private set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public string Avatar { get; set; }
        public bool Ativo { get; set; }
        public bool EmailVerificado { get; set; }   
        public string CodigoVerificacao { get; set; }
        public DateTime? CriadoEm { get; private set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? CodigoVerificacaoExpiracao { get; set; }
        public Usuarios(string nome, string email, string senhaHash)
        {
            Nome = nome;
            Email = email;
            SenhaHash = senhaHash;

            Ativo = true;
            EmailVerificado = false;
            CriadoEm = DateTime.Now;
        }
        public Usuarios() { }
    }
}
