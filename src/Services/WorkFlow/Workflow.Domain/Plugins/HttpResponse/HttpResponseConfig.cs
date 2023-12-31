﻿using Workflow.Domain.Plugins.Attributes;
using Workflow.Domain.Plugins.HttpListener;

namespace Workflow.Domain.Plugins.HttpResponse;

[Immutable, GenerateSerializer]
[Config(PluginTypeNames.HttpResponse)]
public record struct HttpResponseConfig : IPluginConfig
{
    [Id(0)]
    public IReadOnlyCollection<HeaderField>? Headers { get; set; }
    [Id(1)]
    public IReadOnlyCollection<KeyValuePair<string, string>>? StaticHeaders { get; set; }

    /// <summary>
    /// We replace ContentFields using string.Format
    /// For example Content is like: {{ "age": {0}, "name": {1} }}
    /// Note: Use double '{' to escape '{' 
    /// </summary>
    [Id(2)]
    public string? ContentTemplate { get; init; }
    [Id(3)]
    public IReadOnlyCollection<string>? ContentFields { get; set; }
    //TODO status
}