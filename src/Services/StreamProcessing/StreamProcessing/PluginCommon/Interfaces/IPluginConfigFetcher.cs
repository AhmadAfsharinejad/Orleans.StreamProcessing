using Workflow.Domain;

namespace StreamProcessing.PluginCommon.Interfaces;

internal interface IPluginConfigFetcher<TConfig>
{
    Task<TConfig> GetConfig(WorkflowId workflowId, PluginId pluginId);
}