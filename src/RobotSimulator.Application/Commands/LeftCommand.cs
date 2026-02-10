using RobotSimulator.Domain.Interfaces;

namespace RobotSimulator.Application.Commands;

public class LeftCommand : ICommand
{
    public void Execute(IRobot robot) => robot.TurnLeft();
}