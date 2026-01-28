using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using TaskGX.Dados;
using TaskGX.Ferramentas;
using TaskGX.Model;

namespace TaskGX.View
{
    public partial class CriarContaView : Window
    {
        public CriarContaView()
        {
            InitializeComponent();
        }

        private void BotaoMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BotaoFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BotaoLoginGoogle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BotaoCriarConta_Click(object sender, RoutedEventArgs e)
        {
            string nome = TextBoxNome.Text?.Trim();
            string email = TextBoxEmail.Text?.Trim();
            string senha = PasswordBoxSenha.Password;
            string confirmar = PasswordBoxConfirmarSenha.Password;

            if (string.IsNullOrEmpty(nome) ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(senha) ||
                string.IsNullOrEmpty(confirmar))
            {
                MessageBox.Show("Por favor preencha todos os campos.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (senha != confirmar)
            {
                MessageBox.Show("As senhas não coincidem.", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var repo = new RepositorioUsuario();

                if (repo.ExisteEmail(email))
                {
                    MessageBox.Show("Este email já está registado.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var usuario = new Usuarios(nome, email, AjudaHash.GerarHashSenha(senha));
                // O construtor já define Ativo=true e CriadoEm, ajusta se necessário.

                repo.Inserir(usuario);

                MessageBox.Show("Conta criada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                // Em desenvolvimento mostre a exceção; em produção logue-a
                MessageBox.Show("Erro ao criar conta:\n" + ex.Message, "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}