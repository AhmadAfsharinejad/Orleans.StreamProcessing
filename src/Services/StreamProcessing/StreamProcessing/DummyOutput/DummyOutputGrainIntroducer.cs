using StreamProcessing.DummyOutput.Interfaces;
using StreamProcessing.PluginCommon.Interfaces;
using Workflow.Domain;
using Workflow.Domain.Plugins;

namespace StreamProcessing.DummyOutput;

internal sealed class DummyOutputGrainIntroducer : IPluginGrainIntroducer
{
    public PluginTypeId PluginTypeId => new(PluginTypeNames.DummyOutput);
    public Type GrainInterface => typeof(IDummyOutputGrain);
}