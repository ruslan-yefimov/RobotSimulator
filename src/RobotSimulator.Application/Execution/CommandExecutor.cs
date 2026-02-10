using RobotSimulator.Domain.Interfaces;

namespace RobotSimulator.Application.Execution;

public class CommandExecutor : ICommandExecutor
{
    public void Execute(IEnumerable<string> commands, IRobot robot)
    {
        throw new NotImplementedException();
    }
}