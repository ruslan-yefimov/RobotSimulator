namespace RobotSimulator.Domain.Interfaces;

public interface ICommandExecutor
{
    void Execute(IEnumerable<string> commands, IRobot robot);
}