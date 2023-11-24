using StreamProcessing.HttpResponse.Domain;
using StreamProcessing.PluginCommon.Domain;
using Workflow.Domain.Plugins.HttpResponse;

namespace StreamProcessing.HttpResponse.Interfaces;

internal interface IHttpResponseService
{
    HttpResponseTuple GetResponse(HttpResponseConfig config, PluginRecord record);
}