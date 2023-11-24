using Workflow.Domain.Plugins.Common;
using Workflow.Domain.Plugins.HttpListener;

namespace StreamProcessing.HttpListener.Interfaces;

internal interface IHttpListenerOutputFieldTypeGetter
{
    IReadOnlyDictionary<string, FieldType> GetOutputs(HttpListenerConfig config);
}