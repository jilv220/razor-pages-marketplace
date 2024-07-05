using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string UserId { get; set; }

        [ForeignKey("UserId")]
        public required virtual ApplicationUser User { get; set; }

        public required virtual ICollection<CartProduct> CartProducts { get; set; }
    }
}
