using DEMOex.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DEMOex.Helpers
{
    public class ProductSotring
    {
        public static List<Product> SortByPrice(List<Product> products, ComboBox combo)
        {
            switch (combo.SelectedIndex)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        products = products.Where(p => p.ProductDiscountAmount < 10).ToList();
                        break;
                    }
                case 2:
                    {
                        products = products.Where(p => p.ProductDiscountAmount > 10 && p.ProductDiscountAmount < 15).ToList();
                        break;
                    }
                case 3:
                    {
                        products = products.Where(p => p.ProductDiscountAmount > 15).ToList();
                        break;
                    }
            }

            return products;
        }

        public static List<Product> SortByDiscount(List<Product> products, ComboBox combo)
        {
            switch (combo.SelectedIndex)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        products = products.OrderBy(p => p.ProductCost).ToList();
                        break;
                    }
                case 2:
                    {
                        products = products.OrderByDescending(p => p.ProductCost).ToList();
                        break;
                    }
            }

            return products;
        }

        public static List<Product> SortBySearch(List<Product> products, TextBox tb)
        {
            if (!(string.IsNullOrWhiteSpace(tb.Text)))
            {
                products = products.FindAll(p => p.ProductName.Contains(tb.Text));
            }

            return products;
        }
    }
}
