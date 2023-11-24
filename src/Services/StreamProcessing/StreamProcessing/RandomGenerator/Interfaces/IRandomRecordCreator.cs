using StreamProcessing.PluginCommon.Domain;
using Workflow.Domain.Plugins.RandomGenerator;

namespace StreamProcessing.RandomGenerator.Interfaces;

internal interface IRandomRecordCreator
{
    PluginRecord Create(Dictionary<string, RandomType> columnTypesByName);
}