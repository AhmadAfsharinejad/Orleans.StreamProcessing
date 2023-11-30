namespace Workflow.Executor.Domain;

public record ExecutorConfig
{
    public ExecutorConfig()
    {
    }

    public ExecutorConfig(string Url)
    {
        this.Url = Url;
    }

    public string Url { get; init; }

    public void Deconstruct(out string Url)
    {
        Url = this.Url;
    }
}