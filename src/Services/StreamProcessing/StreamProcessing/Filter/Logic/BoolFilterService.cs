using StreamProcessing.Filter.Interfaces;
using Workflow.Domain.Plugins.Common;

namespace StreamProcessing.Filter.Logic;

internal sealed  class BoolFilterService : IEqualFilterService
{
    public FieldType FieldType => FieldType.Bool;

    public bool IsEqual(object? firstValue, object? secondValue)
    {
        if (firstValue is null && secondValue is null) return true;
        if (firstValue is null || secondValue is null) return false;

        return firstValue.Equals(secondValue);
    }
}
