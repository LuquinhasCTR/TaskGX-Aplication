using System;
using System.Security.Cryptography;
using System.Text;
using TaskGX.Dados;
using TaskGX.Model;
using System.Text.RegularExpressions;

using TaskGX.Ferramentas;

namespace TaskGX.Servicos
{
    public class ServicoAutenticacao
    {
        private readonly RepositorioUsuario _usuarioRepository;

        public ServicoAutenticacao()
        {
            _usuarioRepository = new RepositorioUsuario();
        }

        // =========================
        // CRIAR CONTA
        // =========================
        public bool CriarConta(string nome, string email, string senha, string confirmarSenha)
        {
            string _;
            return CriarConta(nome, email, senha, confirmarSenha, out _);
        }

        public bool CriarConta(string nome, string email, string senha, string confirmarSenha, out string mensagemErro)
        {
            mensagemErro = string.Empty;

            nome = nome?.Trim();
            email = email?.Trim();
            senha = senha?.Trim();
            confirmarSenha = confirmarSenha?.Trim();

            // Campos obrigatórios
            if (string.IsNullOrWhiteSpace(nome) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(senha) ||
                string.IsNullOrWhiteSpace(confirmarSenha))
            {
                mensagemErro = "Preencha todos os campos.";
                return false;
            }

            // Email simples
            if (!email.Contains("@"))
            {
                mensagemErro = "Email inválido.";
                return false;
            }

            // Senhas iguais
            if (senha != confirmarSenha)
            {
                mensagemErro = "As senhas não coincidem.";
                return false;
            }

            // Senha forte
            if (!SenhaValida(senha))
            {
                mensagemErro = "Senha fraca. Use 8+ caracteres, ao menos 1 maiúscula e 1 especial.";
                return false;
            }

            try
            {
                // Verificar se já existe
                if (_usuarioRepository.ExisteEmail(email))
                {
                    mensagemErro = "Este email já está cadastrado.";
                    return false;
                }

                // 🔐 Gerar hash seguro
                string senhaHash = AjudaHash.GerarHashSenha(senha);

                Usuarios novoUsuario = new Usuarios
                {
                    Nome = nome,
                    Email = email,
                    Senha = senhaHash,
                    Ativo = true
                };

                _usuarioRepository.Inserir(novoUsuario);
                return true;
            }
            catch (Exception ex)
            {
                mensagemErro = "Erro ao criar conta: " + ex.Message;
                return false;
            }
        }

        // =========================
        // LOGIN
        // =========================
        public bool Login(string email, string senhaDigitada)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senhaDigitada))
                return false;

            email = email.Trim();

            Usuarios usuario;
            try
            {
                usuario = _usuarioRepository.ObterPorEmail(email);
            }
            catch
            {
                return false;
            }

            if (usuario == null)
                return false;

            if (!usuario.Ativo)
                return false;

            // 🔐 Verificar senha com BCrypt
            return AjudaHash.VerificarSenha(
                senhaDigitada,
                usuario.Senha
            );
        }

        // =========================
        // VALIDAÇÃO DE SENHA
        // =========================
        private bool SenhaValida(string senha)
        {
            if (senha.Length < 8)
                return false;

            // Pelo menos uma letra maiúscula
            if (!Regex.IsMatch(senha, "[A-Z]"))
                return false;

            // Pelo menos um caractere especial
            if (!Regex.IsMatch(senha, @"[!@#$%^&*(),.?""':{}|<>_]"))
                return false;

            return true;
        }
    }
}

