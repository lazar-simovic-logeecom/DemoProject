namespace DemoProject.model;

public class Category
{
    private Guid id { get; set; } = new Guid();
    private String title { get; set; }
    private String description { get; set; }
    private Category category { get; set; }
    
}