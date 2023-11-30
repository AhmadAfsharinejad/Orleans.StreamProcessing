namespace Workflow.Application.Designer.Exceptions;

public class NotExistsElementId : Exception
{
    public NotExistsElementId(string id) : base($"Element with id '{id}' no exists.")
    {
        
    }
}