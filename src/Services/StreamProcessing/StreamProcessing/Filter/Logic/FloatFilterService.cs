using StreamProcessing.Filter.Interfaces;
using Workflow.Domain.Plugins.Common;

namespace StreamProcessing.Filter.Logic;

internal sealed class FloatFilterService : DecimalFilterService, IGreaterOrLessFilterService
{
    public override FieldType FieldType => FieldType.Float;
}