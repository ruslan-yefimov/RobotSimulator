using RobotSimulator.Domain.Interfaces;

namespace RobotSimulator.Application.Commands;

public class MoveCommand : ICommand
{
    public void Execute(IRobot robot)
    {
        robot.Move();
    }
}