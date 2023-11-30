
using Workflow.Domain.Plugins.Common;

namespace StreamProcessing.Filter.Interfaces;

internal interface IDataTypeFilterService
{
    FieldType FieldType { get; } 
}