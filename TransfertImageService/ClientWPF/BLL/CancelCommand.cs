using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Diagnostics;

namespace ProjectSoftware.LoginExample.BLL
{
    public class CancelCommand : ICommand
    {

        public void Execute(object parameter)
        {
            LoginViewModel vm = (LoginViewModel)parameter;
            User user = vm.User;

            vm.Error = "";
            vm.User.Username = "test";
            vm.User.Password = "test";            
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;
    }
}
