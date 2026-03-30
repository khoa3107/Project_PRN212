using System;
using System.Collections.Generic;

namespace Project_PRN212.Models;

public partial class ProductionPlanDetail
{
    public int PlanDetailId { get; set; }

    public int PlanId { get; set; }

    public int IngredientId { get; set; }

    public decimal RequiredQuantity { get; set; }

    public virtual Ingredient Ingredient { get; set; } = null!;

    public virtual ProductionPlan Plan { get; set; } = null!;
}
