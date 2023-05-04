using DEMOex.Helpers;
using DEMOex.Models;
using DEMOex.Models.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            tbFrom.Text = _products.Count.ToString();
            tbTo.Text = _products.Count.ToString();
            
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
            sortProducts();
        }

        private void ComboBoxFilterProductByPrice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sortProducts();
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            sortProducts();
        }

        private void sortProducts()
        {
            var sorted = _products;

            sorted = ProductSotring.SortByPrice(sorted, ComboBoxFilterProductByPrice.SelectedIndex);
            sorted = ProductSotring.SortByDiscount(sorted, ComboBoxFilterProductDiscountAmount.SelectedIndex);
            sorted = ProductSotring.SortBySearch(sorted, tbSearch);

            lvProducts.ItemsSource = sorted;
            tbFrom.Text = sorted.Count.ToString();
        }        
    }
}
