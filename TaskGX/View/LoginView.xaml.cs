using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TaskGX.ViewModel;

namespace TaskGX.View
{
    /// <summary>
    /// Lógica interna para LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
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
