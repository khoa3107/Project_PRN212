using System;
using System.Collections.Generic;

namespace Project_PRN212.Models;

public partial class Store
{
    public int StoreId { get; set; }

    public string StoreName { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string? Phone { get; set; }

    public string? ManagerName { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
