﻿namespace Workflow.Domain.Plugins.Common;

[GenerateSerializer]
public enum FieldType
{
    Text,
    Integer,
    Float,
    DateTime,
    Date,
    TimeSpan,
    Bool,
    Guid,
    Bytes
}