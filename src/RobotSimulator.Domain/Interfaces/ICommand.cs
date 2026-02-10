namespace RobotSimulator.Domain.Interfaces;

public interface ICommand
{
    void Execute(IRobot robot);
}