using Workflow.Domain.Plugins.Common;

namespace StreamProcessing.PluginCommon.Domain;

[Immutable]
internal record struct PluginExecutionContext(Guid ScenarioId, Guid PluginId, IReadOnlyDictionary<string, FieldType>? InputFieldTypes);
