using System.Text;
using StreamProcessing.PluginCommon.Domain;
using StreamProcessing.PluginCommon.Interfaces;
using StreamProcessing.Rest.Interfaces;
using Workflow.Domain.Plugins.Rest;
using HttpMethod = Workflow.Domain.Plugins.Rest.HttpMethod;

namespace StreamProcessing.Rest.Logic;

internal sealed class RestRequestCreator : IRestRequestCreator
{
    private readonly IStringReplacer _stringReplacer;
    private readonly IUriReplacer _uriReplacer;

    public RestRequestCreator(IStringReplacer stringReplacer, IUriReplacer uriReplacer)
    {
        _stringReplacer = stringReplacer ?? throw new ArgumentNullException(nameof(stringReplacer));
        _uriReplacer = uriReplacer ?? throw new ArgumentNullException(nameof(uriReplacer));
    }

    public HttpRequestMessage Create(RestConfig config, PluginRecord pluginRecord, CancellationToken cancellationToken)
    {
        HttpRequestMessage request = new(GetNetHttpMethod(config.HttpMethod), _uriReplacer.GetUri(config, pluginRecord));

        AddHeaders(request, config, pluginRecord);

        AddContent(request, config, pluginRecord);

        return request;
    }

    private System.Net.Http.HttpMethod GetNetHttpMethod(HttpMethod httpMethod)
    {
        return new System.Net.Http.HttpMethod(httpMethod.ToString().ToUpper());
    }

    private void AddContent(HttpRequestMessage request, RestConfig config, PluginRecord pluginRecord)
    {
        var content = _stringReplacer.Replace(config.ContentTemplate, config.ContentFields, pluginRecord);

        if (string.IsNullOrWhiteSpace(content)) return;

        request.Content = new StringContent(content, Encoding.UTF8);
    }

    private static void AddHeaders(HttpRequestMessage request, RestConfig config, PluginRecord pluginRecord)
    {
        if (config.RequestHeaders is not null)
        {
            foreach (var headerField in config.RequestHeaders)
            {
                request.Headers.Add(headerField.NameInHeader, pluginRecord.Record[headerField.FieldName].ToString());
            }
        }

        if (config.RequestStaticHeaders is not null)
        {
            foreach (var header in config.RequestStaticHeaders)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }
    }
}