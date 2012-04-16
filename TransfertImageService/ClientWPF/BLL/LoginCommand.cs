using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Diagnostics;
using ClientWPF;

namespace ProjectSoftware.LoginExample.BLL
{
    public class LoginCommand : ICommand
    {

        public void Execute(object parameter)
        {
            LoginViewModel vm = (LoginViewModel)parameter;
            User user = vm.User;
            string path = vm.Path;
            

            // usually this works with a service in ViewModel.
            // something like vm.LoginService(user);
            
            if (user.Username != "username" && user.Password != "password")
            {
                vm.Error = "Login incorect!";
            }
            else
            {
                MainWindow fenetre = new MainWindow(path);
                fenetre.Show();
                //Window1.Close();
            }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
