namespace Workflow.Application.Executer.Interfaces;

public interface IWorkflowExecutor
{
    void Run();
    void Stop();
}