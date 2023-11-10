using StreamProcessing.PluginCommon.Domain;
using Workflow.Domain.Plugins.Common;

namespace StreamProcessing.Filter.Interfaces;

internal interface IFilterService
{
    bool Satisfy(PluginRecord pluginRecord, IConstraint filterConstraint, IReadOnlyDictionary<string, FieldType> inputFieldTypes);
}