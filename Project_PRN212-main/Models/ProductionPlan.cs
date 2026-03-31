using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_PRN212.Models
{
    [Table("ProductionPlans")]
    public class ProductionPlan
    {
        [Key]
        public int PlanId { get; set; }

        [Required]
        [MaxLength(100)]
        public string PlanName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string DishName { get; set; } = string.Empty;

        public int PlannedQuantity { get; set; }

        public DateOnly ProductionDate { get; set; }

        /// <summary>Planned | InProgress | Completed | Cancelled</summary>
        [MaxLength(20)]
        public string Status { get; set; } = "Planned";

        public int CreatedBy { get; set; }

        [MaxLength(255)]
        public string? Note { get; set; }

        // Navigation
        [ForeignKey("CreatedBy")]
        public User? Creator { get; set; }

        public ICollection<ProductionPlanDetail> Details { get; set; } = new List<ProductionPlanDetail>();
        public ICollection<FinishedDish> FinishedDishes { get; set; } = new List<FinishedDish>();
    }
}
