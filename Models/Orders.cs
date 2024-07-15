using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models;
public class Orders
{
    [Key]
    public int Id { get; set; }
    [ForeignKey("ProductId")]
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("UserId")]
    public required string UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; }


}