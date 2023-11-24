using Orleans;

namespace StreamProcessing.Common.Domain;

[Immutable, GenerateSerializer]
public record struct ImmutableWrapper<TConfig>(TConfig Config);
