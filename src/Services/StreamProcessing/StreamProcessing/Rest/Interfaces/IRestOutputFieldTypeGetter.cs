using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.Rest.Domain;
using Workflow.Domain.Plugins.Common;

namespace StreamProcessing.Rest.Interfaces;

internal interface IRestOutputFieldTypeGetter
{
    IReadOnlyDictionary<string, FieldType> GetOutputs(PluginExecutionContext pluginContext, RestConfig config);
}