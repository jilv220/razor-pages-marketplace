using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public string? Description { get; set; }

    [Column(TypeName = "decimal(12, 2)")]
    public required decimal UnitPrice { get; set; }
    public required int QtyInStock { get; set; }
    public string? ImageUri { get; set; }

    public DateTime CreateTime { get; set; }
}