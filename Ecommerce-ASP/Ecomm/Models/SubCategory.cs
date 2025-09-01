using System.Text.Json.Serialization;

namespace Ecomm.Models;

public class SubCategory
{
    public int Id { get; set; }
    public int ParentId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    [JsonIgnore] public virtual Category Category { get; set; }
    public virtual ICollection<ProductSubCategory> ProductCategories { get; set; } = new List<ProductSubCategory>();
}