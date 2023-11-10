using StronglyTypedIds;
using Workflow.Domain.Plugins;

namespace Workflow.Domain;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.String)]
public partial struct PluginTypeId
{
    public PluginTypeId(){}
    
    public PluginTypeId(PluginTypeNames pluginTypeName)
    {
        Value = pluginTypeName.ToString();
    }
    
    public static implicit operator string(PluginTypeId id) => id.Value;
    public static explicit operator PluginTypeId(string id) => new(id);
}