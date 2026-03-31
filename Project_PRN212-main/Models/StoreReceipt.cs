using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_PRN212.Models
{
    [Table("StoreReceipts")]
    public class StoreReceipt
    {
        [Key]
        public int ReceiptId { get; set; }

        public int ShipmentId { get; set; }

        public int ReceivedBy { get; set; }

        public DateTime ReceivedDate { get; set; } = DateTime.Now;

        /// <summary>Received | Rejected</summary>
        [MaxLength(20)]
        public string ReceiptStatus { get; set; } = "Received";

        [MaxLength(255)]
        public string? Note { get; set; }

        // Navigation
        [ForeignKey("ShipmentId")]
        public Shipment? Shipment { get; set; }

        [ForeignKey("ReceivedBy")]
        public User? Receiver { get; set; }
    }
}
