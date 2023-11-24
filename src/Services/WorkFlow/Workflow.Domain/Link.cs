namespace Workflow.Domain;

[Immutable, GenerateSerializer]
public record struct Link
{
    public Link(LinkId Id, PluginIdWithPort Source, PluginIdWithPort Target)
    {
        this.Id = Id;
        this.Source = Source;
        this.Target = Target;
    }

    [Id(0)]
    public LinkId Id { get; set; }
    [Id(1)]
    public PluginIdWithPort Source { get; set; }
    [Id(2)]
    public PluginIdWithPort Target { get; set; }

    public void Deconstruct(out LinkId Id, out PluginIdWithPort Source, out PluginIdWithPort Target)
    {
        Id = this.Id;
        Source = this.Source;
        Target = this.Target;
    }
}