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
using DEMOex.Models;
using DEMOex.Models.Entities;
using DEMOex.Navigation;
using DEMOex.Pages;

namespace DEMOex
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {     
        public AuthWindow()
        {
            InitializeComponent();
            AuthFrame.Navigate(new AuthPage());
            AuthNavigationManager.AuthFrame = AuthFrame;
            AuthNavigationManager.AuthWindow = this;
        }
    }
}
