namespace DemoProject.Dto;
using System.ComponentModel.DataAnnotations;
public class CategoryDto
{ 
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title can't be longer than 100 characters.")]
        public string Title { get; set; }

        [StringLength(100, ErrorMessage = "Description can't be longer than 100 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Code is required.")]
        [StringLength(100, ErrorMessage = "Code can't be longer than 100 characters.")]
        public string Code { get; set; }

        public Guid? ParentCategory { get; set; }
}