using System;
using System.Collections.Generic;

namespace DEMOex.Models.Entities;

public partial class ProductCategory
{
    public int ProductCategoryId { get; set; }

    public string ProductCategoryName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public override string ToString()
    {
        return $"|{ProductCategoryId}| {ProductCategoryName}";
    }
}
