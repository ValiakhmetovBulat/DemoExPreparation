using DEMOex.Models.Entities;
using DEMOex.Models;
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
using DEMOex.Navigation;
using System.Threading;
using System.Windows.Threading;

namespace DEMOex.Pages
{   
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        private int _tickCounter = 0;
        private List<User>? _users;
        private static int _authCount = 0;
        private DispatcherTimer _timer;

        public AuthPage(bool isBlocked = false)
        {
            InitializeComponent();
            var users = ProductDbContext.GetContext().Users.ToList();

            _users = users;

            if (isBlocked)
            {
                var timer = new DispatcherTimer();
                timer.Tick += timer_Tick;
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Start();

                _timer = timer;

                btnAuth.IsEnabled = false;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            _tickCounter++;
            textError.Text = "Доступно через: " + (10 - _tickCounter) + " секунд";
            if (_tickCounter >= 10)
            {
                btnAuth.IsEnabled = true;
                _timer.Stop();
                textError.Text = "";
            }
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            var userToAuth = _users.Find(u => u.UserLogin == tbLogin.Text && u.UserPassword == tbPassword.Text);

            if (userToAuth != null)
            {
                MainWindow mw = new MainWindow(userToAuth);
                mw.Show();
                AuthNavigationManager.AuthWindow.Close();                          
            }
            else
            {
                if (_authCount > 0)
                {
                    AuthNavigationManager.AuthFrame.Navigate(new CaptchaPage(userToAuth));
                }
                textError.Text = "Неверное имя пользователя или пароль";
                _authCount++;
            }
        }
    }
}
