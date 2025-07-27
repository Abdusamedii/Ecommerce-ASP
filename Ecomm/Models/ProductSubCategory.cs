using System.Text.Json.Serialization;

namespace Ecomm.Models;

public class ProductSubCategory
{
    public int ProductId { get; set; }
    public int SubCategoryId { get; set; }
    [JsonIgnore] public virtual Product Product { get; set; }
    [JsonIgnore] public virtual SubCategory SubCategory { get; set; }
}