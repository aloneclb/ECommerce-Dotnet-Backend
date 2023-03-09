using System.ComponentModel.DataAnnotations.Schema;
using ETicaret.Domain.Entities.Base;

namespace ETicaret.Domain.Entities;

public class ProductImage : TimeStampEntity<int>
{
    public string FileUrl { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    
    // [NotMapped] Base class'ta override olduğu için ezebilirsin. Configuration dosyasındada ezebiliriz.
    // public override DateTime UpdatedAt { get; set; }
}