using System;
using System.Collections.Generic;

namespace Project_PRN212.Models;

public partial class StoreReceipt
{
    public int ReceiptId { get; set; }

    public int ShipmentId { get; set; }

    public int ReceivedBy { get; set; }

    public DateTime ReceivedDate { get; set; }

    public string ReceiptStatus { get; set; } = null!;

    public string? Note { get; set; }

    public virtual User ReceivedByNavigation { get; set; } = null!;

    public virtual Shipment Shipment { get; set; } = null!;
}
