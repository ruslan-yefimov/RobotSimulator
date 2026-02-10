using RobotSimulator.Domain.Interfaces;

namespace RobotSimulator.Application.Execution;

public class CommandExecutor(ICommandParser parser) : ICommandExecutor
{
    public void Execute(IEnumerable<string> commands, IRobot robot)
    {
    }
}