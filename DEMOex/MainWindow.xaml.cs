using DEMOex.Models.Entities;
using DEMOex.Navigation;
using DEMOex.Pages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DEMOex
{    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static User _user;

        public MainWindow(User user)
        {
            InitializeComponent();
            MainNavigationManager.MainFrame = MainFrame;
            MainNavigationManager.MainWindow = this;

            _user = user;
            MainNavigationManager.MainFrame.Navigate(new ProductPage(_user));
        }
    }
}
