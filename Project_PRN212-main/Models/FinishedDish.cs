using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_PRN212.Models
{
    [Table("FinishedDishes")]
    public class FinishedDish
    {
        [Key]
        public int DishId { get; set; }

        public int PlanId { get; set; }

        [Required]
        [MaxLength(100)]
        public string DishName { get; set; } = string.Empty;

        public int ProducedQuantity { get; set; }

        [MaxLength(50)]
        public string Unit { get; set; } = "portion";

        public DateOnly ProductionDate { get; set; }

        public DateOnly? ExpiryDate { get; set; }

        /// <summary>Available | Shipped | Expired</summary>
        [MaxLength(20)]
        public string Status { get; set; } = "Available";

        // Navigation
        [ForeignKey("PlanId")]
        public ProductionPlan? Plan { get; set; }

        public ICollection<ShipmentDetail> ShipmentDetails { get; set; } = new List<ShipmentDetail>();
    }
}
