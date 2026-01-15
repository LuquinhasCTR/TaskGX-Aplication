using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TaskGX.ViewModel;
using TaskGX.View;

namespace TaskGX.View
{
    /// <summary>
    /// Lógica interna para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        private readonly LoginViewModel _viewModel;

        public LoginView()
        {
            InitializeComponent();

            _viewModel = new LoginViewModel();
            _viewModel.PropertyChanged += ViewModel_PropertyChanged;

            DataContext = _viewModel;
        }

        private void CriarConta_Click(object sender, RoutedEventArgs e)
        {
            CriarContaView window = new CriarContaView();
            window.Show();
        }

        // 🔔 Escuta mudanças no ViewModel
        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LoginViewModel.LoginSucesso))
            {
                if (_viewModel.LoginSucesso)
                {
                    // ✅ Abre a janela principal
                    var mainWindow = new MainWindow();
                    
                    // Define a MainWindow como janela principal antes de fechar a LoginView
                    // Isso evita que a aplicação encerre quando a LoginView fecha
                    Application.Current.MainWindow = mainWindow;
                    
                    mainWindow.Show();

                    // ✅ Fecha a janela de login
                    Close();
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void TextBoxSenha_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
            {
                vm.SenhaTexto = TextBoxSenha.Password;
            }
        }
        
        private void BotaoMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BotaoFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
