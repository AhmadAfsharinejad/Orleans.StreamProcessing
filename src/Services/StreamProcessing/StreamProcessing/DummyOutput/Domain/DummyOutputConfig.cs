using StreamProcessing.PluginCommon.Domain;

namespace StreamProcessing.DummyOutput.Domain;

[Immutable]
public record struct DummyOutputConfig : IStreamPluginConfig
{
    public DummyOutputConfig()
    {
        RecordCountInterval = 10;
    }
    
    public bool IsWriteEnabled { get; set; }
    public int RecordCountInterval { get; set; }
}