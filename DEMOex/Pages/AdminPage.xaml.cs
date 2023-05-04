using DEMOex.Helpers;
using DEMOex.Models;
using DEMOex.Models.Entities;
using DEMOex.Navigation;
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
    /// Логика взаимодействия для AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private User _user;
        private List<Product> _products;

        public AdminPage(User user)
        {
            InitializeComponent();
            _products = ProductDbContext.GetContext().Products.ToList();
            lvProducts.ItemsSource = _products;

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

            sorted = ProductSotring.SortByPrice(sorted, ComboBoxFilterProductByPrice);
            sorted = ProductSotring.SortByDiscount(sorted, ComboBoxFilterProductDiscountAmount);
            sorted = ProductSotring.SortBySearch(sorted, tbSearch);

            lvProducts.ItemsSource = sorted;
            tbFrom.Text = sorted.Count.ToString();
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            MainNavigationManager.MainFrame.Navigate(new AddEditProductPage(new Product()));
        }

        private void btnEditProduct_Click(object sender, RoutedEventArgs e)
        {
            MainNavigationManager.MainFrame.Navigate(new AddEditProductPage((sender as Button).DataContext as Product));
        }
    }
}
