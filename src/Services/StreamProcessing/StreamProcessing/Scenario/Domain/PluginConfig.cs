using StreamProcessing.PluginCommon.Domain;
using Workflow.Domain;

namespace StreamProcessing.Scenario.Domain;

[Immutable]
public record struct PluginConfig(PluginTypeId PluginTypeId, Guid Id, IStreamPluginConfig Config);
