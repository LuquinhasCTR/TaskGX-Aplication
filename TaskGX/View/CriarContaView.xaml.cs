using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using TaskGX.Dados;
using TaskGX.Ferramentas;
using TaskGX.Model;
using TaskGX.Servicos;

namespace TaskGX.View
{
    public partial class CriarContaView : Window
    {
        private readonly ServicoAutenticacao _servicoAuth;

        public CriarContaView()
        {
            InitializeComponent();
            _servicoAuth = new ServicoAutenticacao();
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
            string nome = TextBoxNome.Text;
            string email = TextBoxEmail.Text;
            string senha = PasswordBoxSenha.Password;
            string confirmar = PasswordBoxConfirmarSenha.Password;

            bool sucesso = _servicoAuth.CriarConta(nome, email, senha, confirmar, out string mensagemErro);

            if (sucesso)
            {
                MessageBox.Show("Conta criada com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
                return;
            }

            MessageBox.Show(
                string.IsNullOrWhiteSpace(mensagemErro)
                    ? "Erro ao criar conta. Verifique os dados."
                    : mensagemErro,
                "Aviso",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
    }
}