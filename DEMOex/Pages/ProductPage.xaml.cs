using DEMOex.Models;
using DEMOex.Models.Entities;
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

namespace DEMOex.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        private User _authUser;
        private List<Product> _products;

        public ProductPage(User user)
        {
            InitializeComponent();
            _products = ProductDbContext.GetContext().Products.ToList();
            lvProducts.ItemsSource = _products;
            _authUser = user;
            
            ComboBoxFilterProductDiscountAmount.ItemsSource = new List<string>
            {
                "Все", "0-10%", "10-15%", "15-∞%"
            };
            ComboBoxFilterProductByPrice.ItemsSource = new List<string>
            {
                "По умолчанию", "По возрастанию", "По убыванию"
            };

            ComboBoxFilterProductDiscountAmount.SelectedIndex = 0;
            ComboBoxFilterProductByPrice.SelectedIndex = 0;
        }

        private void ComboBoxFilterProductDiscountAmount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboBoxFilterProductDiscountAmount.SelectedIndex)
            {
                case 0:
                    {
                        lvProducts.ItemsSource = _products.ToList();
                        break;
                    }
                case 1:
                    {
                        lvProducts.ItemsSource = _products.Where(p => p.ProductDiscountAmount < 10).ToList();
                        break;
                    }
                case 2:
                    {
                        lvProducts.ItemsSource = _products.Where(p => p.ProductDiscountAmount > 10 && p.ProductDiscountAmount < 15).ToList();
                        break;
                    }
                case 3:
                    {
                        lvProducts.ItemsSource = _products.Where(p => p.ProductDiscountAmount > 15).ToList();
                        break;
                    }
            }            
        }

        private void ComboBoxFilterProductByPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboBoxFilterProductByPrice.SelectedIndex)
            {
                case 0:
                    {
                        lvProducts.ItemsSource = _products.ToList();
                        break;
                    }
                case 1:
                    {
                        lvProducts.ItemsSource = _products.Where(p => p.ProductDiscountAmount < 10).ToList();
                        break;
                    }
                case 2:
                    {
                        lvProducts.ItemsSource = _products.Where(p => p.ProductDiscountAmount > 10 && p.ProductDiscountAmount < 15).ToList();
                        break;
                    }
                case 3:
                    {
                        lvProducts.ItemsSource = _products.Where(p => p.ProductDiscountAmount > 15).ToList();
                        break;
                    }
            }
        }
    }
}
