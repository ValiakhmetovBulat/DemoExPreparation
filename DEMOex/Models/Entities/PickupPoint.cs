using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DEMOex.Models.Entities;

public partial class PickupPoint
{
    public int PickupPointId { get; set; }

    public string Address { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public override string ToString()
    {
        return this.Address;
    }
}
