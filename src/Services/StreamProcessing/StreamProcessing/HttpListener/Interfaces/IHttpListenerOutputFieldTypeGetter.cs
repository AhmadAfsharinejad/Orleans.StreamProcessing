using StreamProcessing.HttpListener.Domain;
using Workflow.Domain.Plugins.Common;

namespace StreamProcessing.HttpListener.Interfaces;

internal interface IHttpListenerOutputFieldTypeGetter
{
    IReadOnlyDictionary<string, FieldType> GetOutputs(HttpListenerConfig config);
}