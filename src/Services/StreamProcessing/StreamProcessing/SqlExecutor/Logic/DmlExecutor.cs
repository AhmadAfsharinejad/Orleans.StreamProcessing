using StreamProcessing.SqlExecutor.Interfaces;
using Workflow.Domain.Plugins.SqlExecutor;

namespace StreamProcessing.SqlExecutor.Logic;

internal sealed class DmlExecutor : IDmlExecutor
{
    private readonly ICommandFiller _commandFiller;

    public DmlExecutor(ICommandFiller commandFiller)
    {
        _commandFiller = commandFiller ?? throw new ArgumentNullException(nameof(commandFiller));
    }
    
    public async Task Execute(IStreamDbConnection connection, 
        IStreamDbCommand command,
        DmlCommand dmlCommand, 
        IReadOnlyDictionary<string, object>? record,
        CancellationToken cancellationToken)
    {
        _commandFiller.Fill(connection, command, dmlCommand.CommandText, dmlCommand.ParameterFields, record);
        command.ExecuteNonQuery();
        //Note:ExecuteNonQueryAsync call ExecuteNonQuery and return Task.FromResult!
        await Task.CompletedTask;
    }
}