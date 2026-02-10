using RobotSimulator.Domain.Interfaces;

namespace RobotSimulator.Application.Commands;

public class RightCommand : ICommand
{
    public void Execute(IRobot robot) => robot.TurnRight();
}