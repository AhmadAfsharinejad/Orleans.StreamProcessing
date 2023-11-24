using Workflow.Domain;

namespace Workflow.Application.PluginFinder.Domain;

internal record PluginConfigTypeWithId(PluginTypeId TypeId, Type ConfigType);
