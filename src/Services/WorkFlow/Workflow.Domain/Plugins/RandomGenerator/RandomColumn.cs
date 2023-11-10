using Workflow.Domain.Plugins.Common;

namespace Workflow.Domain.Plugins.RandomGenerator;

public record struct RandomColumn(StreamField Field, RandomType Type);
