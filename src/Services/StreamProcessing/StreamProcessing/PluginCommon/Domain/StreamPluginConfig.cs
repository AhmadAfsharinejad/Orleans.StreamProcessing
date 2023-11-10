namespace StreamProcessing.PluginCommon.Domain;

[Immutable]
public record struct StreamPluginConfig<TConfig> : IStreamPluginConfig
{
    public TConfig Config { get; init; }
}