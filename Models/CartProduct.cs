using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class CartProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CartId { get; set; }

        public required virtual Cart Cart { get; set; }

        [Required]
        public int ProductId { get; set; }

        public required virtual Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
