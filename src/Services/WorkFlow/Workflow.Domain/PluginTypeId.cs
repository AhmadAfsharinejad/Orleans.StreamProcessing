using Workflow.Domain.Plugins;
#pragma warning disable CS8618

namespace Workflow.Domain;

[Immutable, GenerateSerializer]
public record struct PluginTypeId
{
    [Id(0)] public string Value { get; }

    public PluginTypeId()
    {
    }

    public PluginTypeId(string value)
    {
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public PluginTypeId(PluginTypeNames pluginTypeName)
    {
        Value = pluginTypeName.ToString();
    }

    public static implicit operator string(PluginTypeId id) => id.Value;
    public static explicit operator PluginTypeId(string id) => new(id);
}