namespace DemoProject.Application.Exceptions;

public class CategoryHasSubCategoriesException : Exception
{
    public CategoryHasSubCategoriesException(string message) : base(message) { }
}