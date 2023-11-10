using StreamProcessing.PluginCommon.Domain;
using Workflow.Domain.Plugins.Common;
using Workflow.Domain.Plugins.Rest;

namespace StreamProcessing.Rest.Interfaces;

internal interface IRestOutputFieldTypeGetter
{
    IReadOnlyDictionary<string, FieldType> GetOutputs(PluginExecutionContext pluginContext, RestConfig config);
}