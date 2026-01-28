using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TaskGX.Ferramentas;
using TaskGX.Servicos;
using TaskGX.View;
using TaskGX.Dados;


namespace TaskGX.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly ServicoAutenticacao _servicoAuth;

        public LoginViewModel()
        {
            _servicoAuth = new ServicoAutenticacao();
            ComandoLogin = new RelayCommand(Login);
            CriarContaCommand = new RelayCommand(AbrirTelaCriarConta);
            RecuperarContaCommand = new RelayCommand(AbrirTelaCriarRecuperar);
        }
        private void AbrirTelaCriarRecuperar()
        {
            new TaskGX.View.RecuperarContaView().Show();
        }

        private void AbrirTelaCriarConta()
        {
            new TaskGX.View.CriarContaView().Show();
        }

        public ICommand RecuperarContaCommand { get; }

        public ICommand CriarContaCommand { get; }
        public string Email { get; set; }
        public string SenhaTexto { get; set; }

        private bool _loginSucesso;
        public bool LoginSucesso
        {
            get => _loginSucesso;
            private set
            {
                _loginSucesso = value;
                OnPropertyChanged();
            }
        }

        public ICommand ComandoLogin { get; }

        private void Login()
        {
            if (string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(SenhaTexto))
                return;

            bool sucesso = _servicoAuth.Login(Email, SenhaTexto);

            if (!sucesso)
            {
                LoginSucesso = false;
                return;
            }

            // 🔔 ISSO DISPARA O EVENTO NA VIEW
            LoginSucesso = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
