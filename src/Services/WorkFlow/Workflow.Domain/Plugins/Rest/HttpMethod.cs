namespace Workflow.Domain.Plugins.Rest;

[GenerateSerializer]
public enum HttpMethod
{
    Get,
    Put,
    Post,
    Delete,
    Head,
    Options,
    Trace,
    Patch,
    Connect
}