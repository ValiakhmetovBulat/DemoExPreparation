using Accessibility;
using DEMOex.Models;
using DEMOex.Models.Entities;
using DEMOex.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для AddEditProductPage.xaml
    /// </summary>
    public partial class AddEditProductPage : Page
    {
        private Product _product = new Product();
        private ProductDbContext _context = ProductDbContext.GetContext(); 

        public AddEditProductPage(Product product)
        {
            InitializeComponent();

            if (product != null)
                _product = product;

            DataContext = _product;

            comboCategory.ItemsSource = _context.ProductCategories.ToList();
            comboManufacturer.ItemsSource = _context.ProductManufacturers.ToList();
            comboSupplier.ItemsSource = _context.ProductSuppliers.ToList();
            comboUnitType.ItemsSource = _context.UnitTypes.ToList();
            
            if (_product.ProductId != 0)
            {
                comboCategory.SelectedItem = _product.ProductCategory;
                comboManufacturer.SelectedItem = _product.ProductManufacturer;
                comboSupplier.SelectedItem = _product.ProductSupplier;
                comboUnitType.SelectedItem = _product.UnitType;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_product.ProductId == 0)
            {
                _context.Products.Add(_product);
            }

            _product.ProductCategory = comboCategory.SelectedItem as ProductCategory;
            _product.ProductManufacturer = comboManufacturer.SelectedItem as ProductManufacturer;
            _product.ProductSupplier = comboSupplier.SelectedItem as ProductSupplier;
            _product.UnitType = comboUnitType.SelectedItem as UnitType;

            //try
            //{
            _context.SaveChanges();
                MessageBox.Show("Информация сохранена");
                MainNavigationManager.MainFrame.GoBack();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
    }
}
