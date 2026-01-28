using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TaskGX.Ferramentas;
using TaskGX.Servicos;

namespace TaskGX.ViewModel
{
    public class CriarContaViewModel : INotifyPropertyChanged
    {
        private readonly ServicoAutenticacao _servicoAuth;

        public CriarContaViewModel()
        {
            _servicoAuth = new ServicoAutenticacao();
            CriarContaCommand = new RelayCommand(CriarConta);
        }

        // =========================
        // PROPRIEDADES (BINDINGS)
        // =========================
        private string _nome;
        public string Nome
        {
            get => _nome;
            set { _nome = value; OnPropertyChanged(); }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        // ⚠ PasswordBox não faz binding direto
        // Esses valores normalmente vêm via AttachedProperty
        private string _senha;
        public string Senha
        {
            get => _senha;
            set { _senha = value; OnPropertyChanged(); }
        }

        private string _confirmarSenha;
        public string ConfirmarSenha
        {
            get => _confirmarSenha;
            set { _confirmarSenha = value; OnPropertyChanged(); }
        }

        // =========================
        // FEEDBACK PARA A VIEW
        // =========================
        private bool _sucessoCriar;
        public bool SucessoCriar
        {
            get => _sucessoCriar;
            set { _sucessoCriar = value; OnPropertyChanged(); }
        }

        private string _mensagemErro;
        public string MensagemErro
        {
            get => _mensagemErro;
            set { _mensagemErro = value; OnPropertyChanged(); }
        }

        // =========================
        // COMMAND
        // =========================
        public ICommand CriarContaCommand { get; }

        private void CriarConta()
        {
            bool sucesso = _servicoAuth.CriarConta(
                Nome,
                Email,
                Senha,
                ConfirmarSenha
            );

            if (!sucesso)
            {
                SucessoCriar = false;
                MensagemErro = "Erro ao criar conta. Verifique os dados.";
                return;
            }

            SucessoCriar = true;
            MensagemErro = string.Empty;
        }

        // =========================
        // INotifyPropertyChanged
        // =========================
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
