using Workflow.Domain.Plugins.Rest;

namespace StreamProcessing.Rest.Interfaces;

internal interface IRestResponseConverter
{
    Task<IReadOnlyDictionary<string, object>> Convert(HttpResponseMessage responseMessage, RestConfig config);
}