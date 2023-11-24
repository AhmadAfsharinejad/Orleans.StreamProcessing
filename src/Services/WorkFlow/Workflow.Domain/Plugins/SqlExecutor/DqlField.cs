using Workflow.Domain.Plugins.Common;

namespace Workflow.Domain.Plugins.SqlExecutor;

[Immutable, GenerateSerializer]
public record struct DqlField
{
    public DqlField(string DbName, StreamField Field)
    {
        this.DbName = DbName;
        this.Field = Field;
    }

    [Id(0)]
    public string DbName { get; set; }
    [Id(1)]
    public StreamField Field { get; set; }

    public void Deconstruct(out string DbName, out StreamField Field)
    {
        DbName = this.DbName;
        Field = this.Field;
    }
}