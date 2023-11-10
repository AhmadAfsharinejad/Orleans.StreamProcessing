using StreamProcessing.Filter.Interfaces;
using StreamProcessing.PluginCommon.Domain;

namespace StreamProcessing.Filter.Domain;

[Immutable]
public record struct FilterConfig : IStreamPluginConfig
{
    public IConstraint Constraint { get; set; }
}