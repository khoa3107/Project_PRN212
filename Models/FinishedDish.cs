using System;
using System.Collections.Generic;

namespace Project_PRN212.Models;

public partial class FinishedDish
{
    public int DishId { get; set; }

    public int PlanId { get; set; }

    public string DishName { get; set; } = null!;

    public int ProducedQuantity { get; set; }

    public string Unit { get; set; } = null!;

    public DateOnly ProductionDate { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public string Status { get; set; } = null!;

    public virtual ProductionPlan Plan { get; set; } = null!;

    public virtual ICollection<ShipmentDetail> ShipmentDetails { get; set; } = new List<ShipmentDetail>();
}
