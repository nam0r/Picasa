using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;

namespace ProjectSoftware.LoginExample.BLL
{

    public class LoginViewModel : INotifyPropertyChanged
    {
        public LoginViewModel(User user, string path)
        {
            this.Path = path;
            this.User = user;
            this.LoginCommand = new LoginCommand();
            this.CancelCommand = new CancelCommand();
        }
  
        private User m_user;
        public User User
        { 
            get 
            { 
                return m_user; 
            } 
            set 
            { 
                m_user = value; 
                this.OnPropertyChanged("User"); 
            } 
        }
        private string path;
        public String Path
        {
            get
            {
                return path;
            }
            set
            {
                path = value;
                this.OnPropertyChanged("Path"); 
            }
        }

        private string m_error = "";
        public string Error 
        {
            get { return m_error; }
            set { m_error = value; this.OnPropertyChanged("Error"); }
        }

        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
