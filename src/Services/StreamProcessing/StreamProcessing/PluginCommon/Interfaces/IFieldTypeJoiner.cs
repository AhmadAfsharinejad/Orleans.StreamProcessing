using Workflow.Domain.Plugins.Common;

namespace StreamProcessing.PluginCommon.Interfaces;

internal interface IFieldTypeJoiner
{
    IReadOnlyDictionary<string, FieldType> Join(IReadOnlyDictionary<string, FieldType>? inputFieldTypesByName,
        IEnumerable<StreamField>? pluginFields,
        RecordJoinType joinType);
}