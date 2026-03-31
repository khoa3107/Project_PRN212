using System;
using System.Collections.Generic;

namespace Project_PRN212.Models;

public partial class Ingredient
{
    public int IngredientId { get; set; }

    public string IngredientName { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public decimal QuantityInStock { get; set; }

    public decimal? ImportPrice { get; set; }

    public DateTime LastUpdated { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<ProductionPlanDetail> ProductionPlanDetails { get; set; } = new List<ProductionPlanDetail>();
}
