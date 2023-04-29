using DEMOex.Models.Entities;
using DEMOex.Navigation;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace DEMOex.Pages
{
    /// <summary>
    /// Логика взаимодействия для CaptchaPage.xaml
    /// </summary>
    public partial class CaptchaPage : Page
    {
        private User _authUser;
        public CaptchaPage(User user)
        {
            InitializeComponent();
            textCaptcha.Text = GenerateCaptcha(6);
            _authUser = user;
        }

        private string GenerateCaptcha(int length)
        {
            string generatedCaptcha = "";
            Random rand = new Random();

            for (int i = 0; i < length; i++)
            {
                generatedCaptcha += Convert.ToChar(rand.Next(65, 90));
            }
            
            return generatedCaptcha;
        }

        private void btnCaptcha_Click(object sender, RoutedEventArgs e)
        {
            if (tbCaptcha.Text == textCaptcha.Text)
            {
                AuthNavigationManager.AuthFrame.GoBack();
            }
            else
            {
                MessageBox.Show("Была допущена ошибка при вводе CAPTCHA. Вход заблокирован на 10 секунд.");
                AuthNavigationManager.AuthFrame.Navigate(new AuthPage(true));
            }
        }
    }
}
