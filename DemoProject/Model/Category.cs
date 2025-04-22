namespace DemoProject.model;

public class Category
{
    
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Code {get; set;}
    public Guid? ParentCategory { get; set; }

    public Category(string title, string description, string code, Guid? parentCategory)
    {
        Title = title;
        Description = description;
        Code = code;
        ParentCategory = parentCategory;
    }
    
    
}