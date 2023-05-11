using DEMOex.Helpers;
using DEMOex.Models;
using DEMOex.Models.Entities;
using DEMOex.Navigation;
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
        private Order _order = new Order();
        private List<OrderProduct> _orderedProducts = new List<OrderProduct>();

        public ProductPage(User user)
        {
            InitializeComponent();
            _products = ProductDbContext.GetContext().Products.ToList();
            lvProducts.ItemsSource = _products;
            _authUser = user;
            
            _order.UserId = _authUser.UserId;
            _order.OrderStatusId = 1;

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

        private void btnAddProductToOrder_Click(object sender, RoutedEventArgs e)
        {
            var selectedProducts = lvProducts.SelectedItems.Cast<Product>().ToList();

            if (selectedProducts.Count == 0)
            {
                MessageBox.Show("Выберите товар, который желаете заказать нажатием на карточку");
                return;
            }

            foreach (var item in selectedProducts)
            {
                var productToAdd = _orderedProducts.Find(o => o.ProductId == item.ProductId);
                if (productToAdd == null)
                {
                    OrderProduct newOrderProduct = new OrderProduct
                    {
                        OrderId = _order.OrderId,
                        Product = item,
                        ProductId = item.ProductId,
                        Count = 1
                    };
                    _orderedProducts.Add(newOrderProduct);
                }
                else
                {
                    _orderedProducts.Find(o => o.ProductId == item.ProductId).Count++;
                }
            }

            MessageBox.Show("Продукт был добавлен в заказ");
            btnGoToCart.Visibility = Visibility.Visible;
        }

        private void btnGoToCart_Click(object sender, RoutedEventArgs e)
        {
            MainNavigationManager.MainFrame.Navigate(new CartPage(_orderedProducts, _order));
        }
    }
}
