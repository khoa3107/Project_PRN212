using System;
using System.Collections.Generic;

namespace Project_PRN212.Models;

public partial class ShipmentDetail
{
    public int ShipmentDetailId { get; set; }

    public int ShipmentId { get; set; }

    public int DishId { get; set; }

    public int Quantity { get; set; }

    public virtual FinishedDish Dish { get; set; } = null!;

    public virtual Shipment Shipment { get; set; } = null!;
}
