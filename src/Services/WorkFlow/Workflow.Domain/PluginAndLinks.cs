namespace Workflow.Domain;

[Immutable, GenerateSerializer]
public record struct PluginAndLinks
{
    public PluginAndLinks(IReadOnlyCollection<Plugin> Plugins, IReadOnlyCollection<Link> Links)
    {
        this.Plugins = Plugins;
        this.Links = Links;
    }

    [Id(0)]
    public IReadOnlyCollection<Plugin> Plugins { get; set; }
    [Id(1)]
    public IReadOnlyCollection<Link> Links { get; set; }
}