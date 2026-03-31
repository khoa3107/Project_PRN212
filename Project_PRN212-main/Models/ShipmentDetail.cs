using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_PRN212.Models
{
    [Table("ShipmentDetails")]
    public class ShipmentDetail
    {
        [Key]
        public int ShipmentDetailId { get; set; }

        public int ShipmentId { get; set; }

        public int DishId { get; set; }

        public int Quantity { get; set; }

        // Navigation
        [ForeignKey("ShipmentId")]
        public Shipment? Shipment { get; set; }

        [ForeignKey("DishId")]
        public FinishedDish? Dish { get; set; }
    }
}
