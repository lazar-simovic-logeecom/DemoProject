namespace DemoProject.Application.Exceptions;

public class InvalidParentCategoryException : Exception
{
    public InvalidParentCategoryException(string message) : base(message) { }
}