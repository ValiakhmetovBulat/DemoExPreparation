using System;
using System.Collections.Generic;

namespace DEMOex.Models.Entities;

public partial class UnitType
{
    public int UnitTypeId { get; set; }

    public string? UnitTypeName { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public override string ToString()
    {
        return $"{UnitTypeName}";
    }
}
