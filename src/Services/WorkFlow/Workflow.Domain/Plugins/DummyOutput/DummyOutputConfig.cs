using Workflow.Domain.Plugins.Attributes;

namespace Workflow.Domain.Plugins.DummyOutput;

[Config(PluginTypeNames.DummyOutput)]
public record struct DummyOutputConfig : IPluginConfig
{
    public DummyOutputConfig()
    {
        RecordCountInterval = 10;
    }
    
    public bool IsWriteEnabled { get; init; }
    public int RecordCountInterval { get; init; }
}