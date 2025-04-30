namespace DemoProject.Dto;

public class CategoryDto
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required string Code { get; init; }
    public Guid? ParentCategory { get; init; }
}