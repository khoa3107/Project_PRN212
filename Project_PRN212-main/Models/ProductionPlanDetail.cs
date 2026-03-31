using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_PRN212.Models
{
    [Table("ProductionPlanDetails")]
    public class ProductionPlanDetail
    {
        [Key]
        public int PlanDetailId { get; set; }

        public int PlanId { get; set; }

        public int IngredientId { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal RequiredQuantity { get; set; }

        // Navigation
        [ForeignKey("PlanId")]
        public ProductionPlan? Plan { get; set; }

        [ForeignKey("IngredientId")]
        public Ingredient? Ingredient { get; set; }
    }
}
