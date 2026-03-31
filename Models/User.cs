using System;
using System.Collections.Generic;

namespace Project_PRN212.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Role { get; set; } = null!;

    public int? StoreId { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<ProductionPlan> ProductionPlans { get; set; } = new List<ProductionPlan>();

    public virtual ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();

    public virtual Store? Store { get; set; }

    public virtual ICollection<StoreReceipt> StoreReceipts { get; set; } = new List<StoreReceipt>();
}
