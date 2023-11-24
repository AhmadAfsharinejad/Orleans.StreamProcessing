namespace Workflow.Application.Designer.Exceptions;

public class DuplicateElementIdException : Exception
{
    public DuplicateElementIdException(string id) : base($"Element with id '{id}' already exist.")
    {
        
    }
}