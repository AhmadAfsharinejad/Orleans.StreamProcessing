using StreamProcessing.Filter.Interfaces;
using Workflow.Domain.Plugins.Common;

namespace StreamProcessing.Filter.Logic;

internal sealed class TextFilterService : IEqualFilterService
{
    public FieldType FieldType => FieldType.Text;
    
    public bool IsEqual(object? firstValue, object? secondValue)
    {
        return firstValue?.ToString() == secondValue?.ToString();
    }
}