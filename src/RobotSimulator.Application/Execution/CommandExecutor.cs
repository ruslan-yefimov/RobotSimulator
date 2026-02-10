using RobotSimulator.Domain.Interfaces;

namespace RobotSimulator.Application.Execution;

public class CommandExecutor(ICommandParser parser) : ICommandExecutor
{
    public void Execute(IEnumerable<string> commands, IRobot robot)
    {
        foreach (var line in commands)
        {
            var command = parser.Parse(line);
            command?.Execute(robot);
        }
    }
}