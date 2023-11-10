using Workflow.Domain.Plugins.Common;

namespace Workflow.Domain.Plugins.SqlExecutor;

public record struct DqlField(string DbName, StreamField Field);