using Workflow.Domain;

namespace Workflow.Api.Domain;

public record struct PluginIdWithTypeId(Guid Id, String TypeId);
