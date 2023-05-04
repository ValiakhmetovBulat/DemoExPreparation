using DEMOex.Models;
using DEMOex.Models.Entities;
using DEMOex.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DEMOex.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditProductPage.xaml
    /// </summary>
    public partial class AddEditProductPage : Page
    {
        private readonly Page page1;
        private Product _product = new Product();
        private ProductDbContext _context = ProductDbContext.GetContext(); 

        public AddEditProductPage(Product product, Page? page = null)
        {
            InitializeComponent();
            page1 = page;

            _product = product;

            if (_product.ProductId != 0)
            {
                tbProductArticleNumber.IsEnabled = false;                
            }
            else
            {
                tbProductArticleNumber.IsEnabled = true;
            }
            
                

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
            StringBuilder errors = new StringBuilder();
            var stackPanels = addEditGrid.Children.OfType<StackPanel>();
            List<TextBox> textBoxes = new List<TextBox>();
            List<ComboBox> comboBoxes = new List<ComboBox>();

            foreach (StackPanel sp in stackPanels)
            {
                textBoxes.AddRange(sp.Children.OfType<TextBox>());
                comboBoxes.AddRange(sp.Children.OfType<ComboBox>());
            }

            foreach (var item in textBoxes)
            {
                if (string.IsNullOrWhiteSpace(item.Text))
                {
                    errors.AppendLine($"Поле: {item.Name} не может быть пустым");
                }
            }
            foreach (var item in comboBoxes)
            {
                if (item.SelectedItem == null)
                {
                    errors.AppendLine($"Поле: {item.Name} не может быть пустым");
                }
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            _product.ProductCategory = comboCategory.SelectedItem as ProductCategory;
            _product.ProductManufacturer = comboManufacturer.SelectedItem as ProductManufacturer;
            _product.ProductSupplier = comboSupplier.SelectedItem as ProductSupplier;
            _product.UnitType = comboUnitType.SelectedItem as UnitType;

            if (_product.ProductId == 0)
            {
                _context.Products.Add(_product);
            }            

            try
            {
                _context.SaveChanges();
                MessageBox.Show("Информация сохранена");
                ((AdminPage)page1).lvProducts.ItemsSource = _context.Products.ToList();
                ((AdminPage)page1).ComboBoxFilterProductByPrice.SelectedIndex = 0;
                ((AdminPage)page1).ComboBoxFilterProductDiscountAmount.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
}

        private void btnReturnToProductPage_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Не сохраненные данные будут потеряны. Продолжить?", "Возврат на страницу", MessageBoxButton.YesNo, MessageBoxImage.Question)) == MessageBoxResult.Yes)
            {
                MainNavigationManager.MainFrame.GoBack();
            }               
        }
    }
}
