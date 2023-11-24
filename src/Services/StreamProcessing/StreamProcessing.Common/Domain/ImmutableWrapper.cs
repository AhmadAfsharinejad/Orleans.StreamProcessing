using Orleans;

namespace StreamProcessing.Common.Domain;

[Immutable]
public record struct ImmutableWrapper<TConfig>(TConfig Config);
