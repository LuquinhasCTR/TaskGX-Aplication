using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskGX.Ferramentas;

namespace TaskGX.ViewModel
{
    public class LoginViewModel
    {
        public string Nome { get; set; }
        public string SenhaHash { get; set; }
        public ICommand ComandoLogin {get; }

        public LoginViewModel()
        {
            ComandoLogin = new RelayCommand(Login);
        }

        public void Login() { }
    }
}
