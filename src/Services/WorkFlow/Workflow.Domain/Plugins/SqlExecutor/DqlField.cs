using Workflow.Domain.Plugins.Common;

namespace Workflow.Domain.Plugins.SqlExecutor;

[Immutable, GenerateSerializer]
public record struct DqlField(string DbName, StreamField Field);