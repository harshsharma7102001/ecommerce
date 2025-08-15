using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public string PictureUrl { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    public int QuantityInStock { get; set; }
    public string Type { get; set; }
    public string Brand { get; set; }


}
