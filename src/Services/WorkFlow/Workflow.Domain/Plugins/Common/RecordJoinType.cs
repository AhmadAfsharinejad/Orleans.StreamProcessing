namespace Workflow.Domain.Plugins.Common;

[GenerateSerializer]
public enum RecordJoinType
{
    InputOnly,
    ResultOnly,
    Append
}