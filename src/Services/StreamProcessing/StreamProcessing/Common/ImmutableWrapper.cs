namespace StreamProcessing.Common;

[Immutable]
internal record struct ImmutableWrapper<TConfig>(TConfig Config);
