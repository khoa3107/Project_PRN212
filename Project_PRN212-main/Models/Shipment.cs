using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_PRN212.Models
{
    [Table("Shipments")]
    public class Shipment
    {
        [Key]
        public int ShipmentId { get; set; }

        public int StoreId { get; set; }

        public DateOnly ShipmentDate { get; set; }

        public int CreatedBy { get; set; }

        /// <summary>Pending | Shipping | Received | Cancelled</summary>
        [MaxLength(20)]
        public string Status { get; set; } = "Pending";

        [MaxLength(255)]
        public string? Note { get; set; }

        // Navigation
        [ForeignKey("StoreId")]
        public Store? Store { get; set; }

        [ForeignKey("CreatedBy")]
        public User? Creator { get; set; }

        public ICollection<ShipmentDetail> Details { get; set; } = new List<ShipmentDetail>();
        public StoreReceipt? Receipt { get; set; }
    }
}
