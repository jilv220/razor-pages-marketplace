using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models;
public class Orders
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("UserId")]
    public required string UserId { get; set; }

    public DateTime OrderDate { get; set; }


}