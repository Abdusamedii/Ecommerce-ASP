namespace Ecomm.models.DTO;

public class CreateProductDTO
{
    public string description { get; set; }
    public List<string> ImagePath { get; set; }
    public string name { get; set; }

    public float price { get; set; }
    public int quantity { get; set; }

    public List<int> subCategoryId { get; set; }
    public string summary { get; set; }
}