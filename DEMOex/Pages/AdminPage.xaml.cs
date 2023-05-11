using DEMOex.Helpers;
using DEMOex.Models;
using DEMOex.Models.Entities;
using DEMOex.Navigation;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

            sorted = ProductSotring.SortByPrice(sorted, ComboBoxFilterProductByPrice.SelectedIndex);
            sorted = ProductSotring.SortByDiscount(sorted, ComboBoxFilterProductDiscountAmount.SelectedIndex);
            sorted = ProductSotring.SortBySearch(sorted, tbSearch);

            lvProducts.ItemsSource = sorted;
            tbFrom.Text = sorted.Count.ToString();
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            MainNavigationManager.MainFrame.Navigate(new AddEditProductPage(new Product(), this));
        }

        private void btnEditProduct_Click(object sender, RoutedEventArgs e)
        {
            MainNavigationManager.MainFrame.Navigate(new AddEditProductPage((sender as Button).DataContext as Product, this));
        }

        private void btnDeleteProudct_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = lvProducts.SelectedItems.Cast<Product>().ToList();

            if (selectedProduct.Count == 0)
            {
                MessageBox.Show("Выберите товар, который желаете удалить нажатием на карточку");
                return;
            }
            if ((MessageBox.Show("Выбранный товар будет удален. Продолжить?", "Удаление товара", MessageBoxButton.YesNo, MessageBoxImage.Question)) == MessageBoxResult.Yes)
            {                
                var db = ProductDbContext.GetContext();
                db.Products.RemoveRange(selectedProduct);
                db.SaveChanges();
                MessageBox.Show("Товар был успешно удален");
                _products = db.Products.ToList();
                lvProducts.ItemsSource = _products;
                sortProducts();
                tbTo.Text = _products.Count.ToString();
            }
        }
    }
}
