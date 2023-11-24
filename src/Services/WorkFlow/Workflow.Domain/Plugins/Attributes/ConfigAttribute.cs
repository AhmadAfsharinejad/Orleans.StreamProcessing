namespace Workflow.Domain.Plugins.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
public sealed class ConfigAttribute : Attribute
{
    public PluginTypeId PluginTypeId { get; init; }
    
    public ConfigAttribute(PluginTypeNames pluginTypeId)
    {
        PluginTypeId = new PluginTypeId(pluginTypeId);
    }
}