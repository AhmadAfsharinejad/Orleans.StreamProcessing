﻿using StreamProcessing.Filter.Interfaces;
using Workflow.Domain.Plugins.Common;

namespace StreamProcessing.Filter.Logic;

internal sealed class TimeSpanFilterService: IGreaterOrLessFilterService
{
    public FieldType FieldType => FieldType.TimeSpan;
    
    public bool IsEqual(object? firstValue, object? secondValue)
    {
        if (firstValue is null && secondValue is null) return true;
        if (firstValue is null || secondValue is null) return false;
        
        return firstValue.Equals(secondValue);
    }

    public bool IsLess(object? firstValue, object? secondValue)
    {
        if (firstValue is null && secondValue is null) return false;

        if (TryParse(firstValue , out var firstParse) && 
            TryParse(secondValue , out var secondParse) )
        {
            return firstParse < secondParse;
        }

        return false;
    }

    public bool IsGreater(object? firstValue, object? secondValue)
    {
        if (firstValue is null && secondValue is null) return false;

        if (TryParse(firstValue , out var firstParse) && 
            TryParse(secondValue , out var secondParse) )
        {
            return firstParse > secondParse;
        }

        return false;
    }

    private static bool TryParse(object? value, out TimeSpan result)
    {
        if (value is null)
        {
            result = default;
            return false;
        }
        
        if (value is TimeSpan castValue)
        {
            result = castValue;
            return true;
        }

        if (TimeSpan.TryParse(value?.ToString(), out var parsedValue))
        {
            result = parsedValue;
            return true;
        }

        result = default;
        return false;
    }
}