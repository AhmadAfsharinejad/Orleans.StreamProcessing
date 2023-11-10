using Workflow.Domain.Plugins.Common;

namespace StreamProcessing.RandomGenerator.Domain;

public record struct RandomColumn(StreamField Field, RandomType Type);
