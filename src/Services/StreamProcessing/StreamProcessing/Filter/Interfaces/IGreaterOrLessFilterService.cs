namespace StreamProcessing.Filter.Interfaces;

internal interface IGreaterOrLessFilterService : IEqualFilterService
{
    bool IsLess(object? firstValue, object? secondValue);
    bool IsGreater(object? firstValue, object? secondValue);
}