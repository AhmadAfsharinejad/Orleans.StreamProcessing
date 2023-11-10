using StreamProcessing.HttpListener.Interfaces;
using Workflow.Domain.Plugins.Common;
using Workflow.Domain.Plugins.HttpListener;

namespace StreamProcessing.HttpListener.Logic;

internal sealed class HttpListenerOutputFieldTypeGetter : IHttpListenerOutputFieldTypeGetter
{
    public IReadOnlyDictionary<string, FieldType> GetOutputs(HttpListenerConfig config)
    {
        var outputs = new Dictionary<string, FieldType>();

        if (config.Headers is not null)
        {
            foreach (var header in config.Headers)
            {
                outputs[header.FieldName] = FieldType.Text;
            }
        }
        
        if (config.QueryStrings is not null)
        {
            foreach (var queryStrings in config.QueryStrings)
            {
                outputs[queryStrings.FieldName] = FieldType.Text;
            }
        }
        
        if (!string.IsNullOrWhiteSpace(config.ContentFieldName))
        {
            outputs[config.ContentFieldName] = FieldType.Text;
        }

        return outputs;
    }
}