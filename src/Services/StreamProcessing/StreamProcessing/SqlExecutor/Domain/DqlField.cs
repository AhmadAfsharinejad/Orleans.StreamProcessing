using Workflow.Domain.Plugins.Common;

namespace StreamProcessing.SqlExecutor.Domain;

public record struct DqlField(string DbName, StreamField Field);