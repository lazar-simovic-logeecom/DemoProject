namespace DemoProject.Data.Model;

public class Category
{
    public Category(string title, string description, string code, Guid? parentCategory)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Code = code;
        ParentCategory = parentCategory;
        SubCategories = new List<Category>();
    }
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Code {get; set;}
    public Guid? ParentCategory { get; set; }
    public List<Category> SubCategories { get; set; }

    public void Update(Category category)
    {
        Title = category.Title;
        Description = category.Description;
        Code = category.Code;
        ParentCategory = category.ParentCategory;
    }
}