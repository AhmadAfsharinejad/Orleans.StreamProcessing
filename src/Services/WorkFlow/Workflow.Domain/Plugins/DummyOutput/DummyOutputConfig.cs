using Workflow.Domain.Plugins.Attributes;

namespace Workflow.Domain.Plugins.DummyOutput;

[Immutable, GenerateSerializer]
[Config(PluginTypeNames.DummyOutput)]
public record struct DummyOutputConfig : IPluginConfig
{
    public DummyOutputConfig()
    {
        RecordCountInterval = 10;
    }

    [Id(0)]
    public bool IsWriteEnabled { get; init; }
    [Id(1)]
    public int RecordCountInterval { get; init; }
}