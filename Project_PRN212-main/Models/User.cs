using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_PRN212.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        /// <summary>admin | kitchen | store</summary>
        [Required]
        [MaxLength(20)]
        public string Role { get; set; } = string.Empty;

        public int? StoreId { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Active";

        // Navigation
        [ForeignKey("StoreId")]
        public Store? Store { get; set; }
    }
}
