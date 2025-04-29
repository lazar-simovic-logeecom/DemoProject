namespace DemoProject.Application.Exceptions;

public class CategoryHasSubCategoriesException(string message) : Exception(message);
