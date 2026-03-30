using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_PRN212.Models
{
    [Table("Stores")]
    public class Store
    {
        [Key]
        public int StoreId { get; set; }

        [Required]
        [MaxLength(100)]
        public string StoreName { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(100)]
        public string? ManagerName { get; set; }

        [MaxLength(50)]
        public string Status { get; set; } = "Active";

        // Navigation
        public ICollection<User> Users { get; set; } = new List<User>();
        public ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
    }
}
