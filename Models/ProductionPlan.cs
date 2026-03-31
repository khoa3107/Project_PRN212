using System;
using System.Collections.Generic;

namespace Project_PRN212.Models;

public partial class ProductionPlan
{
    public int PlanId { get; set; }

    public string PlanName { get; set; } = null!;

    public string DishName { get; set; } = null!;

    public int PlannedQuantity { get; set; }

    public DateOnly ProductionDate { get; set; }

    public string Status { get; set; } = null!;

    public int CreatedBy { get; set; }

    public string? Note { get; set; }

    public virtual User CreatedByNavigation { get; set; } = null!;

    public virtual ICollection<FinishedDish> FinishedDishes { get; set; } = new List<FinishedDish>();

    public virtual ICollection<ProductionPlanDetail> ProductionPlanDetails { get; set; } = new List<ProductionPlanDetail>();
}
