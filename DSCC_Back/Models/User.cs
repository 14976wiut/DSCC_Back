using System.ComponentModel.DataAnnotations;

namespace DSCC_Back.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Navigation property
        public ICollection<Order> Orders { get; set; }
    }
}
