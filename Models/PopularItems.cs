using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PopularItems
{
    [Key]
    public int ProductId { get; set; }
    public required string ProductName { get; set; }
    public string? Description { get; set; }
    public string? ImageUri { get; set; }
    public int QuantitySold { get; set; }
    public required string Type { get; set; }

    [Column(TypeName = "decimal(12, 2)")]
    public required decimal UnitPrice { get; set; }
    public required int QtyInStock { get; set; }

}
