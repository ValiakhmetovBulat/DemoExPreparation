﻿using System;
using System.Collections.Generic;

namespace DEMOex.Models.Entities;

public partial class ProductManufacturer
{
    public int ProductManufacturerId { get; set; }

    public string ProductManufacturerName { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public override string ToString()
    {
        return $"|{ProductManufacturerId}| {ProductManufacturerName}";
    }
}
