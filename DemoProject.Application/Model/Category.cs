namespace DemoProject.Application.Model;

public class Category()
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Description { get; set; }
    public string Code { get; set; }
    public Guid? ParentCategory { get; set; }
    public List<Category> SubCategories { get; set; } = new();
    public DateTime? DeletedAt { get; set; }

    public void Update(Category category)
    {
        Title = category.Title;
        Description = category.Description;
        Code = category.Code;
    }
}