namespace DemoProject.Dto;

public class ProductDto
{
    public required string Title { get; set; }
    public string Sku { get; set; }
    public string Brand { get; set; }
    public string ShortDescription { get; set; }
    public string LongDescription { get; set; }
    public double Price { get; set; }
    public Guid? CategoryId { get; set; }
}