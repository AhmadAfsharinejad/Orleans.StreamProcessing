using StreamProcessing.Filter.Interfaces;
using Workflow.Domain.Plugins.Common;

namespace StreamProcessing.Filter.Logic;

internal sealed class IntegerFilterService : DecimalFilterService, IGreaterOrLessFilterService
{
    public override FieldType FieldType => FieldType.Integer;
}