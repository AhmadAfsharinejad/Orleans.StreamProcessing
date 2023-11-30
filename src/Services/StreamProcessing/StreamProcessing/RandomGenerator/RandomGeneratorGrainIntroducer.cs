using StreamProcessing.PluginCommon.Interfaces;
using StreamProcessing.RandomGenerator.Interfaces;
using Workflow.Domain;
using Workflow.Domain.Plugins;

namespace StreamProcessing.RandomGenerator;

internal sealed class RandomGeneratorGrainIntroducer : IPluginGrainIntroducer
{
    public PluginTypeId PluginTypeId => new(PluginTypeNames.Random);
    public Type GrainInterface => typeof(IRandomGeneratorGrain);
}