
namespace DemoProject.Dto;

public class CategoryDto
{ 
        public string Title { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        
        public Guid? ParentCategory { get; set; }
}