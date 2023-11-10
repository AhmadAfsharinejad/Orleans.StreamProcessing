using Workflow.Domain;
using Workflow.Domain.Plugins.Common;

namespace StreamProcessing.PluginCommon.Domain;

[Immutable]
internal record struct PluginExecutionContext(WorkflowId WorkFlowId, PluginId PluginId, IReadOnlyDictionary<string, FieldType>? InputFieldTypes);
