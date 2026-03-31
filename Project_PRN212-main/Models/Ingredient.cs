using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_PRN212.Models
{
    [Table("Ingredients")]
    public class Ingredient
    {
        [Key]
        public int IngredientId { get; set; }

        [Required]
        [MaxLength(100)]
        public string IngredientName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Unit { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal QuantityInStock { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal? ImportPrice { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.Now;

        [MaxLength(20)]
        public string Status { get; set; } = "Available";

        // Navigation
        public ICollection<ProductionPlanDetail> ProductionPlanDetails { get; set; } = new List<ProductionPlanDetail>();
    }
}
