using System;
using System.Collections.Generic;

namespace Project_PRN212.Models;

public partial class Shipment
{
    public int ShipmentId { get; set; }

    public int StoreId { get; set; }

    public DateOnly ShipmentDate { get; set; }

    public int CreatedBy { get; set; }

    public string Status { get; set; } = null!;

    public string? Note { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<ShipmentDetail> ShipmentDetails { get; set; } = new List<ShipmentDetail>();

    public virtual Store Store { get; set; } = null!;

    public virtual StoreReceipt? StoreReceipt { get; set; }
}
