﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ProjectSoftware.LoginExample.BLL;

namespace ProjectSoftware.LoginExample
{
    /// <summary>
    /// Logique d'interaction pour LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            // on met des valeurs par défaut dans les champs pour faciliter
            MainGrid.DataContext = new LoginViewModel(
                new User() { Username="username", Password="password"},
                "D:\\"
            );
        }        
    }
}
