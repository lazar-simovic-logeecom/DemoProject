namespace DemoProject.model;
using System.ComponentModel.DataAnnotations;
public class Category
{
    
    public Guid Id { get; set; }
   
    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, ErrorMessage = "Title can't be longer than 100 characters.")]
    public string Title { get; set; }
    
    [StringLength(100, ErrorMessage = "Descritption can't be longer than 100 characters.")]
    public string Description { get; set; }
    
    [Required(ErrorMessage = "Code is required.")]
    [StringLength(100, ErrorMessage = "Code can't be longer than 100 characters.")]
    public string Code {get; set;}
    public Guid? ParentCategory { get; set; }
    public List<Category> SubCategory { get; set; }

    public Category(string title, string description, string code, Guid? parentCategory)
    {
        Title = title;
        Description = description;
        Code = code;
        ParentCategory = parentCategory;
        SubCategory = new List<Category>();
    }
    
    
}