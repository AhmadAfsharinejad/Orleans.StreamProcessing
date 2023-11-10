using Workflow.Domain;

namespace StreamProcessing.PluginCommon.Interfaces;

internal interface IPluginGrainIntroducer
{
    PluginTypeId PluginTypeId { get; }
    
    Type GrainInterface { get; }
}