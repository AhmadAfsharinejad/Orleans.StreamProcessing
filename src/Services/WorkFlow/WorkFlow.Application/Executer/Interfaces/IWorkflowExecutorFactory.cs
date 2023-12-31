﻿using Workflow.Infrastructure.Executer.Interfaces;

namespace Workflow.Application.Executer.Interfaces;

internal interface IWorkflowExecutorFactory
{
    IWorkflowExecutor Create();
}