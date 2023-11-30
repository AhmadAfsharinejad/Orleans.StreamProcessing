using StreamProcessing.Filter.Interfaces;
using StreamProcessing.PluginCommon.Interfaces;
using Workflow.Domain;
using Workflow.Domain.Plugins;

namespace StreamProcessing.Filter;

internal sealed class FilterGrainIntroducer : IPluginGrainIntroducer
{
    public PluginTypeId PluginTypeId => new(PluginTypeNames.Filter);
    public Type GrainInterface => typeof(IFilterGrain);
}