using Workflow.Domain;

namespace StreamProcessing.PluginCommon.Domain;

[Immutable]
internal record struct PluginTypeWithId(PluginId PluginId, PluginTypeId PluginTypeId);
