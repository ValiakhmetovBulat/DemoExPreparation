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
    /// Логика взаимодействия для CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        private List<OrderProduct> _orderedProducts;
        private decimal? _totalSum;
        private decimal? _totalDiscountSum;
        private Order _order;

        public CartPage(List<OrderProduct> orderedProducts, Order order)
        {
            InitializeComponent();

            _orderedProducts = orderedProducts;
            _order = order;
            dataGridCart.ItemsSource = _orderedProducts;

            setTotalSum();     
            
            tbTotalSum.Text = _totalSum.ToString();
            tbTotalDiscountSum.Text = _totalDiscountSum.ToString();

            comboPickupPoints.ItemsSource = ProductDbContext.GetContext().PickupPoints.ToList();
        }

        private void setTotalSum()
        {
            _totalSum = 0;
            _totalDiscountSum = 0;
            foreach (var item in _orderedProducts)
            {
                _totalSum += item.Product.ProductCost;
                _totalDiscountSum += item.Product.ProductDiscountCost;
            }
        }

        private void btnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            var countProducts = 0;
            var db = ProductDbContext.GetContext();

            _order.PickupPoint = comboPickupPoints.SelectedItem as PickupPoint;
            _order.OrderCreateDate = DateTime.Now;

            Random rand = new Random();
            _order.OrderGetCode = rand.Next(100, 999);

            int orderId;

            foreach (var item in _orderedProducts)
            {
                countProducts++;
                if (item.Product.ProductQuantityInStock < 1)
                {
                    _order.OrderDeliveryDate = DateTime.Now.AddDays(6);

                    db.Orders.Add(_order);
                    db.SaveChanges();
                    orderId = db.Orders.OrderBy(o => o.OrderId).Last().OrderId;
                    foreach (var product in _orderedProducts)
                    {
                        product.OrderId = orderId;
                    }
                    db.OrderProducts.AddRange(_orderedProducts);
                    db.SaveChanges();

                    MainNavigationManager.MainFrame.Navigate(new TalonPage());
                    return;
                }                    
            }

            if (countProducts > 3)
            {
                _order.OrderDeliveryDate = DateTime.Now.AddDays(3);
            }
            else
            {
                _order.OrderDeliveryDate = DateTime.Now.AddDays(6);
            }


            db.Orders.Add(_order);
            db.SaveChanges();
            orderId = db.Orders.OrderBy(o => o.OrderId).Last().OrderId;
            foreach (var product in _orderedProducts)
            {
                product.OrderId = orderId;
            }
            db.OrderProducts.AddRange(_orderedProducts);
            db.SaveChanges();

            MainNavigationManager.MainFrame.Navigate(new TalonPage());
        }

        private void btnGoBack_Click(object sender, RoutedEventArgs e)
        {
            MainNavigationManager.MainFrame.GoBack();
        }
    }
}
