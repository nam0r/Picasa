using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace ProjectSoftware.LoginExample.BLL
{
    public class User : IDataErrorInfo, INotifyPropertyChanged
    {
        private string m_username;
        public string Username
        {
            get { return m_username; }
            set 
            { 
                m_username = value; 
                this.OnPropertyChanged("Username"); 
            }
        }

        private string m_password;
        public string Password
        {
            get { return m_password; }
            set 
            { 
                m_password = value; 
                this.OnPropertyChanged("Password"); 
            }
        }


        #region IDataErrorInfo Members

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Username")
                {
                    if (string.IsNullOrEmpty(Username))
                        return "Username cannot be empty";
                }
                if (columnName == "Password")
                {
                    if (string.IsNullOrEmpty(Password))
                        return "Password cannot be empty";
                }
                return "";
            }
        }

        #endregion



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
