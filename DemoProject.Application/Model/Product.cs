namespace DemoProject.Application.Model;

public class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Sku { get; set; }
    public string Brand { get; set; }
    public string ShortDescription { get; set; }
    public string LongDescription { get; set; }
    public double Price { get; set; }
    public Guid? CategoryId { get; set; }
    public DateTime? GetTime { get; set; } = null;

    public void Update(Product product)
    {
        Title = product.Title;
        Sku = product.Sku;
        Brand = product.Brand;
        ShortDescription = product.ShortDescription;
        LongDescription = product.LongDescription;
        Price = product.Price;
    }
}