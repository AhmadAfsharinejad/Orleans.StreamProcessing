namespace StreamProcessing.Common.Domain;

//TODO Delete it?
[Immutable, GenerateSerializer]
public record struct ImmutableWrapper<TConfig>(TConfig Config);
